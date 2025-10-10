using Decidir.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Decidir.Clients
{
    public enum EHttpMethod
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    internal sealed class RestClientHttpFactory : IRestClient
    {
        private static readonly Encoding _encoding = Encoding.GetEncoding("iso-8859-1");
        private readonly string _endpoint;
        private readonly Dictionary<string, string> headers;
        private string _contentType;

        private readonly string CONTENT_TYPE_APP_JSON = "application/json";

        private readonly IHttpClientFactory _httpClientFactory; 
        public RestClientHttpFactory(IHttpClientFactory httpClientFactory, string endpoint, Dictionary<string, string> headers)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));   
            this._endpoint = endpoint;
            this.headers = new Dictionary<string, string>();

            if (headers != null)
            {
                foreach (var key in headers.Keys)
                {
                    this.headers.Add(key, headers[key]);
                }
            }
        }

        public RestClientHttpFactory(IHttpClientFactory httpClientFactory, string endpoint, Dictionary<string, string> headers, string contentType) : this(httpClientFactory, endpoint, headers)
        {
            this._contentType = contentType;
        }
        private HttpClient CreateClient(string uri)
        {
            var client = _httpClientFactory.CreateClient(DecidirAbstractConnectionSettings.DecidirHttpClientFactoryClientName);
            client.BaseAddress = new Uri(uri);
            if (headers != null && headers.Any())
            {
                foreach (string key in headers.Keys)
                {
                    client.DefaultRequestHeaders.Add(key, this.headers[key]);
                }
            }
            return client;
        }
        private StringContent GetStringContent(string data)
        {
            return new StringContent(data, _encoding, string.IsNullOrEmpty(_contentType) ? CONTENT_TYPE_APP_JSON : _contentType);
        }


        public void AddHeaders(Dictionary<string, string> headers)
        {
            if (headers is null) { return; }
            foreach (var key in headers.Keys)
            {
                this.headers.Add(key, headers[key]);
            }
        }

        public void AddContentType(string contentType)
        {
            this._contentType = contentType;
        }

        public RestResponse Get(string url, string data) => GetAsync(url, data).GetAwaiter().GetResult();
        public RestResponse Post(string url, string data) => PostAsync(url, data).GetAwaiter().GetResult();   
        public RestResponse Delete(string url) => DeleteAsync(url).GetAwaiter().GetResult();
        public RestResponse Put(string url, string data = null) => PutAsync(url,data).GetAwaiter().GetResult();

       
        private async Task<RestResponse> DoRequest(EHttpMethod httpMethod, string url, string data = null, CancellationToken cancellationToken = default)
        {
            using (var client = CreateClient(_endpoint))
            {           
                RestResponse result = new RestResponse();
                result.Response = String.Empty;

                try
                {
                    HttpResponseMessage response = null;
                    switch (httpMethod)
                    {
                        case EHttpMethod.GET:
                            response = await client.GetAsync(url, cancellationToken);
                            break;
                        case EHttpMethod.POST:
                            response = await client.PostAsync(url, GetStringContent(data), cancellationToken);
                            break;
                        case EHttpMethod.PUT:
                            response = await client.PutAsync(url, GetStringContent(data), cancellationToken);
                            break;
                        case EHttpMethod.DELETE:
                            response = await client.DeleteAsync(url, cancellationToken);
                            break;
                        default:
                            throw new NotSupportedException($"El método HTTP {httpMethod} no es soportado.");
                    }
                    var resultstr = await response.Content.ReadAsStringAsync();

                    result.StatusCode = (int)response.StatusCode;

                    if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created
                        && response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != HttpStatusCode.Accepted)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        result.Response = message;
                        return result;
                    }
                    result.Response = resultstr;
                }
                catch (HttpRequestException ex)
                {
                    // Problema de red, DNS, TLS, etc.
                    result.StatusCode = 500;
                    result.Response = ex.Message;
                }
                catch (TaskCanceledException ex)
                {
                    // Timeout o cancelación manual
                    result.StatusCode = 408; // Request Timeout (o lo que vos quieras)
                    result.Response = ex.Message;
                }
                catch (Exception ex)
                {
                    result.StatusCode = 500;
                    result.Response = ex.Message;
                }
                return result;
                 
            }

        }
        public async Task<RestResponse> GetAsync(string url, string data, CancellationToken cancellationToken = default)
        {
            return await DoRequest(EHttpMethod.GET, url, null, cancellationToken);
        }
        public async Task<RestResponse> PostAsync(string url, string data, CancellationToken cancellationToken = default)
        {
            return await DoRequest(EHttpMethod.POST, url, data, cancellationToken);
        }

        public async Task<RestResponse> DeleteAsync(string url, CancellationToken cancellationToken = default)
        {
            return await DoRequest(EHttpMethod.DELETE, url, null, cancellationToken);   
        }
        public async Task<RestResponse> PutAsync(string url, string data = null, CancellationToken cancellationToken = default)
        {
            return await DoRequest(EHttpMethod.PUT, url, data, cancellationToken);  
        }


    }
}
