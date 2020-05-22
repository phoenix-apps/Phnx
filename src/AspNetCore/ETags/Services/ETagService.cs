using Phnx.Reflection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Phnx.AspNetCore.ETags.Services
{
    /// <summary>
    /// Manages ETags and caching
    /// </summary>
    public class ETagService : IETagService
    {
        /// <summary>
        /// Check whether an ETag matches a given data model
        /// </summary>
        /// <param name="requestETag">The ETags from the request</param>
        /// <param name="model">The database model to compare the request's ETag to</param>
        /// <returns><see langword="true"/> if the resource is not a match</returns>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/></exception>
        public ETagMatchResult CheckETags(string requestETag, object model)
        {
            if (string.IsNullOrWhiteSpace(requestETag))
            {
                return ETagMatchResult.ETagNotInRequest;
            }
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            string dataETag;

            // Weak ETags
            if (requestETag.ToUpperInvariant().StartsWith("W/"))
            {
                dataETag = GetWeakETag(model);
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
            if (!TryGetStrongETag(model, out dataETag))
            {
                // Strong ETags are not supported
                return ETagMatchResult.StrongDoNotMatch;
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
        /// Get the strong ETag for <paramref name="model"/> by loading the value of the first member which has a <see cref="ConcurrencyCheckAttribute"/>
        /// </summary>
        /// <param name="model">The data to load the strong ETag for</param>
        /// <param name="eTag"><see langword="null"/> if a strong ETag could not be loaded, otherwise, the strong ETag that represents <paramref name="model"/></param>
        /// <returns><see langword="true"/> if a concurrency check property or field is found, or <see langword="false"/> if one is not found</returns>
        public bool TryGetStrongETag(object model, out string eTag)
        {
            if (model is null)
            {
                eTag = null;
                return false;
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
                    eTag = null;
                    return false;
                }

                eTag = $"\"{value}\"";
                return true;
            }

            eTag = null;
            return false;
        }

        /// <summary>
        /// Generates a weak ETag for <paramref name="model"/> by reflecting on its members and hashing them
        /// </summary>
        /// <param name="model">The object to generate a weak ETag for</param>
        /// <returns>A weak ETag for <paramref name="model"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/></exception>
        public string GetWeakETag(object model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var json = JsonSerializer.Serialize(model);
            var jsonBytes = Encoding.UTF8.GetBytes(json);

            var md5 = MD5.Create();
            var hashed = md5.ComputeHash(jsonBytes);

            return $"W/\"{Encoding.UTF8.GetString(hashed)}\"";
        }
    }
}
