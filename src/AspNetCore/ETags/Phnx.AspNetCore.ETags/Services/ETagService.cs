using Phnx.AspNetCore.ETags.Models;
using Phnx.Reflection;
using Phnx.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Phnx.AspNetCore.ETags.Services
{
    /// <summary>
    /// Manages E-Tags and caching
    /// </summary>
    public class ETagService : IETagService
    {
        /// <summary>
        /// Check whether an e-tag matches a given data model
        /// </summary>
        /// <param name="requestETag">The e-tags from the request</param>
        /// <param name="dataModel">The database model to compare the request's e-tag to</param>
        /// <returns><see langword="true"/> if the resource is not a match</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dataModel"/> is <see langword="null"/></exception>
        public ETagMatchResult CheckETagsForModel(string requestETag, object dataModel)
        {
            if (string.IsNullOrWhiteSpace(requestETag))
            {
                return ETagMatchResult.ETagNotInRequest;
            }
            if (dataModel is null)
            {
                throw new ArgumentNullException(nameof(dataModel));
            }

            string dataETag;

            // Weak ETags
            if (requestETag.ToUpperInvariant().StartsWith("W/"))
            {
                dataETag = GetWeakETagForModel(dataModel);
                if (dataETag == requestETag)
                {
                    return ETagMatchResult.WeakMatch;
                }
                else
                {
                    return ETagMatchResult.WeakDoNotMatch;
                }
            }

            // Strong ETags
            if (!TryGetStrongETagForModel(dataModel, out dataETag))
            {
                // Strong ETags are not supported
                return ETagMatchResult.ETagNotInRequest;
            }

            if (dataETag == requestETag)
            {
                return ETagMatchResult.StrongMatch;
            }
            else
            {
                return ETagMatchResult.StrongDoNotMatch;
            }
        }

        /// <summary>
        /// Get the strong e-tag for <paramref name="model"/> by loading the value of the first member which has a <see cref="ConcurrencyCheckAttribute"/>
        /// </summary>
        /// <param name="model">The data to load the strong e-tag for</param>
        /// <param name="etag"><see langword="null"/> if a strong e-tag could not be loaded, otherwise, the strong e-tag that represents <paramref name="model"/></param>
        /// <returns><see langword="true"/> if a concurrency check property or field is found, or <see langword="false"/> if one is not found</returns>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/></exception>
        public bool TryGetStrongETagForModel(object model, out string etag)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var propertyFields = model.GetType().GetPropertyFieldInfos();

            foreach (var propertyField in propertyFields)
            {
                var attr = propertyField.Member.GetAttribute<ConcurrencyCheckAttribute>();

                if (attr is null) continue;

                // Load member data
                var member = propertyField;

                object value;
                try
                {
                    value = member.GetValue(model);
                }
                catch
                {
                    etag = null;
                    return false;
                }

                etag = $"\"{value}\"";
                return true;
            }

            etag = null;
            return false;
        }

        /// <summary>
        /// Generates a weak ETag for <paramref name="model"/> by reflecting on its members and hashing them
        /// </summary>
        /// <param name="model">The object to generate a weak ETag for</param>
        /// <returns>A weak ETag for <paramref name="model"/></returns>
        public string GetWeakETagForModel(object model)
        {
            if (model is null) return string.Empty;

            var json = JsonSerializer.Serialize(model);
            var jsonBytes = Encoding.UTF8.GetBytes(json);

            var shaFactory = new SHA256Managed();
            var hashed = shaFactory.ComputeHash(jsonBytes);

            return $"W/\"{Encoding.UTF8.GetString(hashed)}\"";
        }
    }
}