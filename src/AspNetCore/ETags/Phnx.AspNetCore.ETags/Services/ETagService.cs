using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using Phnx.AspNetCore.ETags.Models;
using Phnx.Reflection;
using Phnx.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
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
        /// The ETag header's key
        /// </summary>
        public const string ETagHeaderKey = "ETag";

        /// <summary>
        /// The IfNoneMatch header's key
        /// </summary>
        public const string IfNoneMatchKey = "If-None-Match";

        /// <summary>
        /// The IfMatch header's key
        /// </summary>
        public const string IfMatchKey = "If-Match";

        /// <summary>
        /// The action context accessor for accessing the current request and response headers
        /// </summary>
        public IActionContextAccessor ActionContext { get; }

        /// <summary>
        /// The headers in the request
        /// </summary>
        public IHeaderDictionary RequestHeaders => ActionContext.ActionContext.HttpContext.Request.Headers;

        /// <summary>
        /// The headers in the response
        /// </summary>
        public IHeaderDictionary ResponseHeaders => ActionContext.ActionContext.HttpContext.Response.Headers;

        /// <summary>
        /// Create a new <see cref="ETagService"/> using a given <see cref="IActionContextAccessor"/>
        /// </summary>
        /// <param name="actionContext">The action context accessor for reading and writing E-Tag headers</param>
        /// <exception cref="ArgumentNullException"><paramref name="actionContext"/> is <see langword="null"/></exception>
        public ETagService(IActionContextAccessor actionContext)
        {
            ActionContext = actionContext ?? throw new ArgumentNullException(nameof(actionContext));
        }

        /// <summary>
        /// Check whether a sent E-Tag does not match a given data model using the If-None-Match header.
        /// Defaults to <see langword="true"/> if the header is not present
        /// </summary>
        /// <param name="data">The data model to compare the E-Tag against</param>
        /// <returns><see langword="true"/> if the resource is not a match</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        public ETagMatchResult CheckIfNoneMatch(object data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (!RequestHeaders.TryGetValue(IfNoneMatchKey, out StringValues eTag) || eTag.Count == 0)
            {
                return ETagMatchResult.ETagNotInRequest;
            }

            return GetETagMatchResult(eTag[0], data);
        }

        /// <summary>
        /// Check whether a sent E-Tag matches a given data model using the If-Match header.
        /// Defaults to <see langword="true"/> if the header is not present
        /// </summary>
        /// <param name="data">The data model to compare the E-Tag against</param>
        /// <returns><see langword="true"/> if the resource is a match</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        public ETagMatchResult CheckIfMatch(object data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (!RequestHeaders.TryGetValue(IfMatchKey, out StringValues eTag) || eTag.Count == 0)
            {
                return ETagMatchResult.ETagNotInRequest;
            }

            return GetETagMatchResult(eTag[0], data);
        }

        /// <summary>
        /// Create the E-Tag response for a match
        /// </summary>
        /// <returns>The E-Tag response for a match</returns>
        public StatusCodeResult CreateMatchResponse()
        {
            return new StatusCodeResult((int)HttpStatusCode.NotModified);
        }

        /// <summary>
        /// Create the E-Tag response for a do not match
        /// </summary>
        /// <returns>The E-Tag response for a do not match</returns>
        public StatusCodeResult CreateDoNotMatchResponse()
        {
            return new StatusCodeResult((int)HttpStatusCode.PreconditionFailed);
        }

        /// <summary>
        /// Append the relevant E-Tag for the data model to the response
        /// </summary>
        /// <param name="data">The data model for which to generate the E-Tag</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/></exception>
        public void AddETagToResponse(object data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (!TryGetStrongETag(data, out string etag))
            {
                etag = GetWeakETag(data);
            }

            ResponseHeaders.Add(ETagHeaderKey, etag);
        }

        private ETagMatchResult GetETagMatchResult(string headerETag, object data)
        {
            string dataETag;

            // Weak ETags
            if (headerETag.ToUpperInvariant().StartsWith("W/"))
            {
                dataETag = GetWeakETag(data);
                if (dataETag == headerETag)
                {
                    return ETagMatchResult.WeakMatch;
                }
                else
                {
                    return ETagMatchResult.WeakDoNotMatch;
                }
            }

            // Strong ETags
            if (!TryGetStrongETag(data, out dataETag))
            {
                // Strong ETags are not supported
                return ETagMatchResult.ETagNotInRequest;
            }

            if (dataETag == headerETag)
            {
                return ETagMatchResult.StrongMatch;
            }
            else
            {
                return ETagMatchResult.StrongDoNotMatch;
            }
        }

        /// <summary>
        /// Get whether a model supports strong ETags by checking if any members have a <see cref="ConcurrencyCheckAttribute"/>
        /// </summary>
        /// <returns><see langword="null"/> if no member is found, or the first <see cref="PropertyFieldInfo"/> which has a <see cref="ConcurrencyCheckAttribute"/></returns>
        public bool TryGetStrongETag(object data, out string etag)
        {
            var propertyFields = data.GetType().GetPropertyFieldInfos();

            foreach (var propertyField in propertyFields)
            {
                var attr = propertyField.Member.GetAttribute<ConcurrencyCheckAttribute>();

                if (attr is null) continue;

                // Load member data
                var member = propertyField;

                object value;
                try
                {
                    value = member.GetValue(data);
                }
                catch
                {
                    etag = null;
                    return false;
                }

                etag = "\"" + value.ToString() + "\"";
                return true;
            }

            etag = null;
            return false;
        }

        /// <summary>
        /// Generates a weak ETag for <paramref name="o"/> by reflecting on its members and hashing its value
        /// </summary>
        /// <param name="o">The object to generate a weak ETag for</param>
        /// <returns>A weak ETag for <paramref name="o"/></returns>
        public string GetWeakETag(object o)
        {
            if (o is null) return string.Empty;

            var json = JsonSerializer.Serialize(o);
            var jsonBytes = Encoding.UTF8.GetBytes(json);

            var shaFactory = new SHA256Managed();
            var hashed = shaFactory.ComputeHash(jsonBytes);

            return "W/\"" + Encoding.UTF8.GetString(hashed) + "\"";
        }
    }
}