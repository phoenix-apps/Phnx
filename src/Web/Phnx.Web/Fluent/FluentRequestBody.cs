using Phnx.Web.Models;
using Phnx.Web.Services;
using Newtonsoft.Json;
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
            this.request = request;
        }

        private void ToContent(string data, string mediaType)
        {
            ToContent(data, mediaType, defaultEncoding);
        }

        private void ToContent(string data, string mediaType, Encoding encoding)
        {
            request.Request.Content = new StringContent(data, encoding, mediaType);
        }

        /// <summary>
        /// Use a JSON format body content, with a specified body
        /// </summary>
        /// <param name="body">The data to send as body content</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        public FluentRequest Json(object body)
        {
            var bodyAsString = JsonConvert.SerializeObject(body);
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
            ToContent(body, ContentType.Text.Plain);

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
            ToContent(body, ContentType.Text.Plain, encoding);

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
            request.Request.Content = body;

            return request;
        }

        /// <summary>
        /// Use a specified content type, with a specified body
        /// </summary>
        /// <param name="contentType">The MIME type of content to use. Use <see cref="ContentType"/> for a collection of helpers</param>
        /// <param name="body">The data to send as body content</param>
        /// <returns>The source <see cref="FluentRequest"/></returns>
        public FluentRequest Custom(string contentType, string body)
        {
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
            ToContent(body, contentType, encoding);

            return request;
        }
    }
}
