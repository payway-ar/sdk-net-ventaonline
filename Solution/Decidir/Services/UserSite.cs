using Decidir.Clients;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Decidir.Model;

namespace Decidir.Services
{
    internal class UserSite : Service
    {
        public UserSite(string endpoint, string privateApiKey, Dictionary<string, string> headers) : base(endpoint)
        {
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
