using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Decidir.Clients
{
    internal class RestClient
    {
        protected string endpoint;
        protected Dictionary<string, string> headers;
        protected string contentType;

        protected const string CONTENT_TYPE_APP_JSON = "application/json";

        protected const string METHOD_POST = "POST";
        protected const string METHOD_GET = "GET";
        protected const string METHOD_PUT = "PUT";
        protected const string METHOD_DELETE = "DELETE";

        public RestClient(string endpoint, Dictionary<string, string> headers)
        {
            this.endpoint = endpoint;
            this.headers = new Dictionary<string, string>();

            if (headers != null)
            {
                foreach (var key in headers.Keys)
                {
                    this.headers.Add(key, headers[key]);
                }
            }
        }

        public RestClient(string endpoint, Dictionary<string, string> headers, string contentType) : this(endpoint, headers)
        {
            this.contentType = contentType;
        }

        public void AddHeaders(Dictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var key in headers.Keys)
                {
                    this.headers.Add(key, headers[key]);
                }
            }
        }

        public void AddContentType(string contentType)
        {
            this.contentType = contentType;
        }

        public RestResponse Get(string url, string data)
        {
            string uri = endpoint + url + data;

            var httpWebRequest = Initialize(uri, METHOD_GET);

            return DoRequest(httpWebRequest);
        }

        public RestResponse Post(string url, string data)
        {
            string uri = endpoint + url;

            var httpWebRequest = Initialize(uri, METHOD_POST);

            if (!string.IsNullOrEmpty(data))
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(data);
                httpWebRequest.ContentLength = bytes.Length;

                using (var writeStream = httpWebRequest.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            return DoRequest(httpWebRequest);
        }

        public RestResponse Delete(string url)
        {
            string uri = endpoint + url;

            var httpWebRequest = Initialize(uri, METHOD_DELETE);
            httpWebRequest.ContentType = null;

            return DoRequest(httpWebRequest);
        }

        public RestResponse Put(string url, string data = null)
        {
            string uri = endpoint + url;

            var httpWebRequest = Initialize(uri, METHOD_PUT);

            if (!string.IsNullOrEmpty(data))
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(data);
                httpWebRequest.ContentLength = bytes.Length;

                using (var writeStream = httpWebRequest.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            return DoRequest(httpWebRequest);
        }

        protected HttpWebRequest Initialize(string uri, string method)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = method;

            httpWebRequest.ContentLength = 0;

            if (!String.IsNullOrEmpty(contentType))
                httpWebRequest.ContentType = contentType;
            else
                httpWebRequest.ContentType = CONTENT_TYPE_APP_JSON;

            SetHeaders(httpWebRequest);

            return httpWebRequest;
        }

        protected RestResponse DoRequest(HttpWebRequest httpWebRequest)
        {
            RestResponse result = new RestResponse();
            result.Response = String.Empty;

            try
            {
                using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    result.StatusCode = ((int)response.StatusCode);

                    if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created
                        && response.StatusCode != HttpStatusCode.NoContent)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        result.Response = message;
                    }
                    else
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new StreamReader(responseStream))
                                {
                                    result.Response = reader.ReadToEnd();
                                }
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    result.StatusCode = (int)((HttpWebResponse)ex.Response).StatusCode;
                    using (var responseStream = ((HttpWebResponse)ex.Response).GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                result.Response = reader.ReadToEnd();
                            }
                        }
                    }
                }
                else
                {
                    result.StatusCode = 500;
                    result.Response = ex.Message;
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Response = ex.Message;
            }

            return result;
        }

        protected void SetHeaders(HttpWebRequest httpWebRequest)
        {
            if (this.headers != null && this.headers.Count > 0)
            {
                foreach (string key in this.headers.Keys)
                {
                    httpWebRequest.Headers.Add(key, this.headers[key]);
                }
            }
        }
    }
}
