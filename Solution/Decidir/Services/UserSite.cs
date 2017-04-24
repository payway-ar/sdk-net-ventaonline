using Decidir.Clients;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Decidir.Model;

namespace Decidir.Services
{
    internal class UserSite : Service
    {
        private string privateApiKey;

        public UserSite(string endpoint, string privateApiKey) : base(endpoint)
        {
            this.privateApiKey = privateApiKey;

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("apikey", this.privateApiKey);
            headers.Add("Cache-Control", "no-cache");

            this.restClient = new RestClient(this.endpoint, headers, CONTENT_TYPE_APP_JSON);
        }

        public GetAllCardTokensResponse GetAllTokens(string userId)
        {
            GetAllCardTokensResponse tokens = null;

            RestResponse result = this.restClient.Get("usersite", String.Format("/{0}/cardtokens", userId));
            if (result.StatusCode == STATUS_OK && !String.IsNullOrEmpty(result.Response))
            {
                tokens = JsonConvert.DeserializeObject<GetAllCardTokensResponse>(result.Response);
            }

            return tokens;
        }
    }
}
