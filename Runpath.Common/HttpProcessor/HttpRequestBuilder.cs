using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Runpath.Common.HttpProcessor
{
    public class HttpRequestBuilder
    {
        private HttpMethod _method = null;
        private string _requestUri = "";
        private readonly HttpContent _content = null;
        private readonly string _acceptHeader = "application/json";
        private readonly TimeSpan _timeout = new TimeSpan(0, 0, 15);
        private readonly bool _allowAutoRedirect = false;
        private readonly List<KeyValuePair<string, string>> _formContentValues = null;

        public HttpRequestBuilder()
        {
        }

        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this._method = method;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this._requestUri = requestUri;
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            // Check required arguments
            EnsureArguments();

            // Set up request
            var request = new HttpRequestMessage
            {
                Method = this._method,
                RequestUri = new Uri(this._requestUri)
            };

            if (this._content != null)
                request.Content = this._content;

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(this._acceptHeader))
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(this._acceptHeader));

            if (this._formContentValues != null && this._formContentValues.Count > 0)
                request.Content = new FormUrlEncodedContent(this._formContentValues);

            // Setup client
            var handler = new HttpClientHandler { AllowAutoRedirect = this._allowAutoRedirect };

            var client = new System.Net.Http.HttpClient(handler) { Timeout = this._timeout };

            return await client.SendAsync(request);
        }

        #region " Private "

        private void EnsureArguments()
        {
            if (this._method == null)
                throw new ArgumentNullException($"Method");

            if (string.IsNullOrEmpty(this._requestUri))
                throw new ArgumentNullException($"Request Uri");
        }

        #endregion
    }
}
