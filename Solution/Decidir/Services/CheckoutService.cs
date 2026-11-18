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

        public HistoryResponse GetHistory(HistoryRequest historyRequest)
        {
            HistoryResponse historyResponse = new HistoryResponse();
            this.Headers["apikey"] = this.PrivateApiKey;
            this.RestClientCheckout = new RestClient(this.EndPointCheckout, this.Headers, CONTENT_TYPE_APP_JSON);

            string queryString = BuildHistoryQueryString(historyRequest);
            string endpoint = string.IsNullOrEmpty(queryString) ? "get-history" : "get-history?" + queryString;

            RestResponse result = this.RestClientCheckout.Get(endpoint);

            Console.WriteLine("RESULTADO DE CONSULTA DE HISTORIAL: " + result.StatusCode + " " + result.Response);

            if (!String.IsNullOrEmpty(result.Response))
            {
                if (result.StatusCode != STATUS_OK)
                {
                    if (result.StatusCode == STATUS_ERROR)
                    {
                        throw new CheckoutHistoryException(
                            result.StatusCode + " - " + result.Response,
                            JsonConvert.DeserializeObject<CheckoutHistoryError>(result.Response),
                            result.StatusCode);
                    }
                    else if (isErrorResponse(result.StatusCode))
                    {
                        throw new CheckoutHistoryException(
                            result.StatusCode.ToString(),
                            JsonConvert.DeserializeObject<CheckoutHistoryError>(result.Response),
                            result.StatusCode);
                    }
                }

                try
                {
                    historyResponse = JsonConvert.DeserializeObject<HistoryResponse>(result.Response);
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
            }

            return historyResponse;
        }

        private string BuildHistoryQueryString(HistoryRequest request)
        {
            if (request == null) return string.Empty;

            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(request.cuit)) queryParams.Add("cuit=" + Uri.EscapeDataString(request.cuit));
            if (!string.IsNullOrEmpty(request.site_id)) queryParams.Add("site_id=" + Uri.EscapeDataString(request.site_id));
            if (!string.IsNullOrEmpty(request.operation_id)) queryParams.Add("operation_id=" + Uri.EscapeDataString(request.operation_id));
            if (!string.IsNullOrEmpty(request.status)) queryParams.Add("status=" + Uri.EscapeDataString(request.status));
            if (!string.IsNullOrEmpty(request.from_date)) queryParams.Add("from_date=" + Uri.EscapeDataString(request.from_date));
            if (!string.IsNullOrEmpty(request.to_date)) queryParams.Add("to_date=" + Uri.EscapeDataString(request.to_date));
            if (!string.IsNullOrEmpty(request.platform)) queryParams.Add("platform=" + Uri.EscapeDataString(request.platform));
            if (request.page.HasValue) queryParams.Add("page=" + request.page.Value);
            if (request.items_per_page.HasValue) queryParams.Add("items_per_page=" + request.items_per_page.Value);
            if (!string.IsNullOrEmpty(request.description)) queryParams.Add("description=" + Uri.EscapeDataString(request.description));
            if (!string.IsNullOrEmpty(request.currency)) queryParams.Add("currency=" + Uri.EscapeDataString(request.currency));
            if (request.establishment_number.HasValue) queryParams.Add("establishment_number=" + request.establishment_number.Value);
            if (request.max_amount.HasValue) queryParams.Add("max_amount=" + request.max_amount.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (request.min_amount.HasValue) queryParams.Add("min_amount=" + request.min_amount.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (request.installments.HasValue) queryParams.Add("installments=" + request.installments.Value);
            if (request.payment_method_id.HasValue) queryParams.Add("payment_method_id=" + request.payment_method_id.Value);

            return string.Join("&", queryParams);
        }

        public List<TransactionDto> GetTransactionHistory(TransactionHistoryRequest transactionHistoryRequest)
        {
            List<TransactionDto> transactionHistoryResponse = new List<TransactionDto>();
            this.Headers["apikey"] = this.PrivateApiKey;
            this.RestClientCheckout = new RestClient(this.EndPointCheckout, this.Headers, CONTENT_TYPE_APP_JSON);

            string endpoint = string.IsNullOrEmpty(transactionHistoryRequest)
                ? "transaction/history"
                : "transaction/history?charge_id=" + Uri.EscapeDataString(transactionHistoryRequest);

            RestResponse result = this.RestClientCheckout.Get(endpoint);

            Console.WriteLine("RESULTADO DE CONSULTA DE HISTORIAL DE TRANSACCIONES: " + result.StatusCode + " " + result.Response);

            if (!String.IsNullOrEmpty(result.Response))
            {
                if (result.StatusCode != STATUS_OK)
                {
                    if (result.StatusCode == STATUS_ERROR)
                    {
                        throw new CheckoutHistoryException(
                            result.StatusCode + " - " + result.Response,
                            JsonConvert.DeserializeObject<CheckoutHistoryError>(result.Response),
                            result.StatusCode);
                    }
                    else if (isErrorResponse(result.StatusCode))
                    {
                        throw new CheckoutHistoryException(
                            result.StatusCode.ToString(),
                            JsonConvert.DeserializeObject<CheckoutHistoryError>(result.Response),
                            result.StatusCode);
                    }
                }

                try
                {
                    transactionHistoryResponse = JsonConvert.DeserializeObject<List<TransactionDto>>(result.Response);
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
            }

            return transactionHistoryResponse;
        }

}
