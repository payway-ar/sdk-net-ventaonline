using Decidir.Clients;
using Decidir.Exceptions;
using Decidir.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Decidir.Services
{
    internal class CardTokens : Service
    {
        private string privateApiKey;

        public CardTokens(string endpoint, string privateApiKey) : base(endpoint)
        {
            this.privateApiKey = privateApiKey;

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("apikey", this.privateApiKey);
            headers.Add("Cache-Control", "no-cache");

            this.restClient = new RestClient(this.endpoint, headers, CONTENT_TYPE_APP_JSON);
        }

        public bool DeleteCardToken(string tokenizedCard)
        {
            bool deleted = false;
            RestResponse result = this.restClient.Delete(String.Format("cardtokens/{0}", tokenizedCard));

            if (result.StatusCode == STATUS_NOCONTENT)
            {
                deleted = true;
            }
            else
            {
                if (isErrorResponse(result.StatusCode))
                    throw new ResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorResponse>(result.Response));
                else
                    throw new ResponseException(result.StatusCode + " - " + result.Response);
            }

            return deleted;
        }
    }
}
