using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MarkSFrancis.Web.Models.Response
{
    public class ApiResponseMessage
    {
        public ApiResponseMessage(HttpResponseMessage message)
        {
            _message = message;
            _body = new Lazy<string>(LoadBody);
        }

        private readonly HttpResponseMessage _message;

        private readonly Lazy<string> _body;

        public HttpStatusCode StatusCode => _message.StatusCode;

        public bool IsSuccessStatusCode => _message.IsSuccessStatusCode;

        public HttpResponseHeaders Headers => _message.Headers;

        public string Body => _body.Value;

        private string LoadBody()
        {
            using (HttpContent content1 = _message.Content)
            {
                return content1.ReadAsStringAsync().Result;
            }
        }

        public void ThrowIfNotSuccessStatus()
        {
            if (!IsSuccessStatusCode)
            {
                throw CreateError();
            }
        }

        public void ThrowIfStatusCodeIsNot(params HttpStatusCode[] successCodes)
        {
            if (!successCodes.Contains(StatusCode))
            {
                throw CreateError();
            }
        }

        private HttpRequestException CreateError()
        {
            return new HttpRequestException(
                $"{(int)StatusCode} ({StatusCode}), Body: {Body}");
        }
    }
}