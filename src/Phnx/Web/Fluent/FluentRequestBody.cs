using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Phnx.Web.Fluent
{
    /// <summary>
    /// A fluent request body builder to help with fluent web requests
    /// </summary>
    public class FluentRequestBody
    {
        private readonly FluentRequest _request;
        private static readonly Encoding _defaultEncoding = Encoding.UTF8;

        internal FluentRequestBody(FluentRequest request)
        {
            Debug.Assert(request != null);

            _request = request;
        }

        /// <summary>
        /// Use a JSON format body content, with a specified body in UTF-8 encoding
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        public FluentRequest Json<T>(T body) => Json(body, _defaultEncoding);

        /// <summary>
        /// Use a JSON format body content, with a specified body
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <param name="encoding">The text encoding to use</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="encoding"/> is <see langword="null"/></exception>
        public FluentRequest Json<T>(T body, Encoding encoding)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            var bodyAsString = body is null ? string.Empty : JsonSerializer.Serialize(body);
            ToContent(ContentType.Application.Json, bodyAsString, encoding);

            return _request;
        }

        /// <summary>
        /// Use a plaintext format body content, with a specified body in UTF-8 encoding
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        public FluentRequest PlainText(string body) => PlainText(body, _defaultEncoding);

        /// <summary>
        /// Use a plaintext format body content, with a specified body
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <param name="encoding">The text encoding to use</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="encoding"/> is <see langword="null"/></exception>
        public FluentRequest PlainText(string body, Encoding encoding)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            ToContent(ContentType.Text.Plain, body ?? string.Empty, encoding);

            return _request;
        }

        /// <summary>
        /// Use an url-encoded form format body content, with a specified body in UTF-8 encoding
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        public FluentRequest Form(object body) => Form(body, _defaultEncoding);

        /// <summary>
        /// Use an url-encoded form format body content, with a specified body
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <param name="encoding">The text encoding to use</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="encoding"/> is <see langword="null"/></exception>
        public FluentRequest Form(object body, Encoding encoding)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            var bodyAsQueryString = UrlSerializer.ToQueryString(body);
            ToContent(ContentType.Application.Form, bodyAsQueryString, encoding);

            return _request;
        }

        /// <summary>
        /// Use a custom content
        /// </summary>
        /// <param name="content">The content of the request</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="content"/> is <see langword="null"/></exception>
        public FluentRequest Custom(HttpContent content)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            _request.Request.Content = content;

            return _request;
        }

        /// <summary>
        /// Use a specified content type, with a specified body in UTF-8 encoding
        /// </summary>
        /// <param name="contentType">The MIME type of content to use. Use <see cref="ContentType"/> for a collection of helpers</param>
        /// <param name="body">The data to send as body content</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="contentType"/> is <see langword="null"/></exception>
        /// <exception cref="FormatException"><paramref name="contentType"/> is in an invalid format</exception>
        public FluentRequest Custom(string contentType, string body) => Custom(contentType, body, _defaultEncoding);

        /// <summary>
        /// Use a specified content type, with a specified body
        /// </summary>
        /// <param name="contentType">The MIME type of content to use. Use <see cref="ContentType"/> for a collection of helpers</param>
        /// <param name="body">The data to send as body content</param>
        /// <param name="encoding">The text encoding to use</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="contentType"/> or <paramref name="encoding"/> is <see langword="null"/></exception>
        /// <exception cref="FormatException"><paramref name="contentType"/> is in an invalid format</exception>
        public FluentRequest Custom(string contentType, string body, Encoding encoding)
        {
            if (contentType is null)
            {
                throw new ArgumentNullException(nameof(contentType));
            }
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            ToContent(contentType, body, encoding);

            return _request;
        }

        /// <exception cref="FormatException"><paramref name="contentType"/> is in an invalid format</exception>
        private void ToContent(string contentType, string body, Encoding encoding)
        {
            Debug.Assert(contentType != null);
            Debug.Assert(encoding != null);

            _request.Request.Content = new StringContent(body ?? string.Empty, encoding, contentType);
        }
    }
}
