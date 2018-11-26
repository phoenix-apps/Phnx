using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace Phnx.Web.Fluent
{
    /// <summary>
    /// A fluent request body builder to help with fluent web requests
    /// </summary>
    public class FluentRequestBody
    {
        private readonly FluentRequest request;
        private static readonly Encoding defaultEncoding = Encoding.UTF8;

        internal FluentRequestBody(FluentRequest request)
        {
            Debug.Assert(request != null);

            this.request = request;
        }

        /// <summary>
        /// Use a JSON format body content, with a specified body
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        public FluentRequest Json(object body)
        {
            var bodyAsString = body is null ? string.Empty : JsonConvert.SerializeObject(body);
            ToContent(bodyAsString, ContentType.Application.Json);

            return request;
        }

        /// <summary>
        /// Use a plaintext format body content, with a specified body with UTF-8 encoding
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        public FluentRequest PlainText(string body)
        {
            ToContent(body ?? string.Empty, ContentType.Text.Plain);

            return request;
        }

        /// <summary>
        /// Use a plaintext format body content, with a specified body
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <param name="encoding">The text encoding to use</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        public FluentRequest PlainText(string body, Encoding encoding)
        {
            ToContent(body ?? string.Empty, ContentType.Text.Plain, encoding);

            return request;
        }

        /// <summary>
        /// Use an url-encoded form format body content, with a specified body
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        public FluentRequest Form(object body)
        {
            var bodyAsQueryString = UrlSerializer.ToQueryString(body);
            ToContent(bodyAsQueryString, ContentType.Application.Form);

            return request;
        }

        /// <summary>
        /// Use an url-encoded form format body content, with a specified body
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <param name="encoding">The text encoding to use</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        public FluentRequest Form(object body, Encoding encoding)
        {
            var bodyAsQueryString = UrlSerializer.ToQueryString(body);
            ToContent(bodyAsQueryString, ContentType.Application.Form, encoding);

            return request;
        }

        /// <summary>
        /// Use a specified content type, with a specified body
        /// </summary>
        /// <param name="body">The body of the request</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        public FluentRequest Custom(HttpContent body)
        {
            if (body is null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            request.Request.Content = body;

            return request;
        }

        /// <summary>
        /// Use a specified content type, with a specified body
        /// </summary>
        /// <param name="contentType">The MIME type of content to use. Use <see cref="ContentType"/> for a collection of helpers</param>
        /// <param name="body">The data to send as body content</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        /// <remarks>See <see cref="ContentType"/> for MIME content type helpers</remarks>
        public FluentRequest Custom(string contentType, string body)
        {
            if (contentType is null)
            {
                throw new ArgumentNullException(nameof(contentType));
            }
            if (body is null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            ToContent(body, contentType);

            return request;
        }

        /// <summary>
        /// Use a specified content type, with a specified body
        /// </summary>
        /// <param name="contentType">The MIME type of content to use. Use <see cref="ContentType"/> for a collection of helpers</param>
        /// <param name="body">The data to send as body content</param>
        /// <param name="encoding">The text encoding to use</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        public FluentRequest Custom(string contentType, string body, Encoding encoding)
        {
            if (contentType is null)
            {
                throw new ArgumentNullException(nameof(contentType));
            }
            if (body is null)
            {
                throw new ArgumentNullException(nameof(body));
            }
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            ToContent(body, contentType, encoding);

            return request;
        }

        private void ToContent(string data, string mediaType)
        {
            ToContent(data, mediaType, defaultEncoding);
        }

        private void ToContent(string data, string mediaType, Encoding encoding)
        {
            Debug.Assert(mediaType != null);
            Debug.Assert(encoding != null);

            request.Request.Content = new StringContent(data ?? string.Empty, encoding, mediaType);
        }
    }
}
