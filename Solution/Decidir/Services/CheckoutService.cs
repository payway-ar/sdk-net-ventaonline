using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Decidir.Clients;
using Decidir.Exceptions;
using Decidir.Model;
using Newtonsoft.Json;

namespace Decidir.Services
{
    internal class CheckoutService : Service
    {
        private string PrivateApiKey;
        private RestClient RestClientCheckout;
        Dictionary<string, string> Headers;
        private string EndPointCheckout;

        public CheckoutService(string Endpoint, string PrivateApiKey, Dictionary<string, string> Headers) : base(Endpoint)
        {
            this.PrivateApiKey = PrivateApiKey;
            this.EndPointCheckout = Endpoint;
            this.restClient = new RestClient(this.EndPointCheckout, this.Headers, CONTENT_TYPE_APP_JSON);
            this.Headers = Headers;
        }
        public CheckoutResponse CheckoutHash(CheckoutRequest checkoutRequest)
        {
            CheckoutResponse checkoutResponse = new CheckoutResponse();
            this.Headers["apikey"] = this.PrivateApiKey;
            this.RestClientCheckout = new RestClient(this.EndPointCheckout, this.Headers, CONTENT_TYPE_APP_JSON);
            RestResponse result = this.RestClientCheckout.Post("payments/link", CheckoutRequest.toJson(checkoutRequest));

            Console.WriteLine("RESULTADO DE GENERACION DE LINK: " + result.StatusCode + " " + result.Response);

            if (!String.IsNullOrEmpty(result.Response))
            {
                try
                {

                    checkoutResponse.response = JsonConvert.DeserializeObject<CheckoutGenerateHashResponse>
                           (result.Response);
                }
                catch (JsonReaderException j)
                {
                    Console.WriteLine("ERROR DE CASTEO: " + j.ToString());
                    ErrorResponse ErrorPaymentResponse = new ErrorResponse();
                    ErrorPaymentResponse.code = "502";
                    ErrorPaymentResponse.error_type = "Error en recepción de mensaje";
                    ErrorPaymentResponse.message = "No se pudo leer la respuesta";
                    ErrorPaymentResponse.validation_errors = null;
                    throw new Exception(ErrorPaymentResponse.code);
                }

                if (checkoutResponse != null)
                {

                    checkoutResponse.statusCode = result.StatusCode;
                }

                if (result.StatusCode != STATUS_CREATED)
                {
                    if (result.StatusCode == STATUS_ERROR)
                    {
                        throw new CheckoutResponseException(result.StatusCode + " - " + result.Response, JsonConvert.DeserializeObject<ErrorCheckoutResponse>(result.Response), result.StatusCode);
                    }
                    else
                    {

                        if (isErrorResponse(result.StatusCode))
                        {

                            throw new CheckoutResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorCheckoutResponse>(result.Response), result.StatusCode);
                        }
                    }

                }
            }
            return checkoutResponse;
        }
    }
}
