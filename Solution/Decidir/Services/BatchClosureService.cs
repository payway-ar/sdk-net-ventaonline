using Decidir.Clients;
using Decidir.Exceptions;
using Decidir.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Services
{
    internal class BatchClosure : Service
    {
        private string privateApiKey;
        private string publicApiKey;
        private string validateApiKey;
        private string merchant;
        private string request_host;
        private RestClient restClientValidate;
        private RestClient restClientGetTokenBSA;

        public BatchClosure(String endpoint, String privateApiKey, String validateApiKey = null, String merchant = null, string request_host = null, string publicApiKey = null) : base(endpoint)
        {
            this.privateApiKey = privateApiKey;
            this.validateApiKey = validateApiKey;
            this.merchant = merchant;
            this.request_host = request_host;
            this.publicApiKey = publicApiKey;


            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("apikey", this.privateApiKey);
            headers.Add("Cache-Control", "no-cache");

            this.restClient = new RestClient(this.endpoint, headers, CONTENT_TYPE_APP_JSON);
        }

        public BatchClosureResponse BatchClosureActive(String batchClosure)
        {
            BatchClosureResponse refund = null;


            RestResponse result = this.restClient.Post(String.Format("closures/batchclosure"), batchClosure);

            if (result.StatusCode == STATUS_CREATED && !String.IsNullOrEmpty(result.Response))
            {
                refund = JsonConvert.DeserializeObject<BatchClosureResponse>(result.Response);
            }
            else
            {
                throw new ResponseException(result.StatusCode + " - " + result.Response);
            }

            return refund;

        }
    }
}