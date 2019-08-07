using Decidir.Clients;
using Decidir.Model;
using Decidir.Exceptions;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Decidir.Services
{
    internal class HealthCheck : Service
    {
        public HealthCheck(string endpoint) : base(endpoint)
        {
            this.restClient = new RestClient(this.endpoint, new Dictionary<string, string>(), CONTENT_TYPE_APP_JSON);
        }

        public HealthCheckResponse Execute()
        {
            HealthCheckResponse response = new HealthCheckResponse();
            RestResponse result = this.restClient.Get("healthcheck", "");

            if (result.StatusCode == STATUS_OK && !String.IsNullOrEmpty(result.Response))
            {
                response = HealthCheckResponse.toHealthCheckResponse(result.Response);
            }
            else
            {
                if (isErrorResponse(result.StatusCode))
                    throw new ResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorResponse>(result.Response));
                else
                    throw new ResponseException(result.StatusCode + " - " + result.Response);
            }
             
            return response;
        }
    }
}
