using Decidir.Clients;
using Decidir.Exceptions;
using Decidir.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decidir.Services
{
    internal class Payments : Service
    {
        private string privateApiKey;
        private string publicApiKey;
        private string validateApiKey;
        private string merchant;
        private string request_host;
        private RestClient restClientValidate;
        private RestClient restClientGetTokenBSA;
        Dictionary<string, string> headers;

        public Payments(String endpoint, String privateApiKey, Dictionary<string, string> headers, String validateApiKey=null , String merchant=null, string request_host = null, string publicApiKey = null) : base(endpoint)
        {
            this.privateApiKey = privateApiKey;
            this.validateApiKey = validateApiKey;
            this.merchant = merchant;
            this.request_host = request_host;
            this.publicApiKey = publicApiKey;
            this.headers = headers;
            this.restClient = new RestClient(this.endpoint, this.headers, CONTENT_TYPE_APP_JSON);
        }

        public PaymentResponse ExecutePayment(OfflinePayment payment)
        {
            Payment paymentCopy = payment.copyOffline();

            return DoPayment(paymentCopy);
        }

        public PaymentResponse ExecutePayment(Payment payment)
        {
            /*Payment paymentCopy = payment.copy();*/

            /*return DoPayment(paymentCopy);*/
            return DoPayment(payment);

        }


        public PaymentResponse InstructionThreeDS(string xConsumerUsername, Instruction3dsData instruction3DsData)
        {
            return sendInstructionThreeDS(xConsumerUsername, instruction3DsData);
        }

        public CapturePaymentResponse CapturePayment(long paymentId, double amount)
        {
            int amountCapture = Convert.ToInt32(amount * 100);
            CapturePaymentResponse response = null;
            RestResponse result = this.restClient.Put(String.Format("payments/{0}", paymentId.ToString()), "{\"amount\": " + amountCapture.ToString() + " }");

            if (result.StatusCode != STATUS_NOCONTENT && result.StatusCode != STATUS_OK)
            {
                if (isErrorResponse(result.StatusCode))
                    throw new ResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorResponse>(result.Response));
                else
                    throw new ResponseException(result.StatusCode + " - " + result.Response);
            }
            else
            {
                if (!String.IsNullOrEmpty(result.Response))
                {
                    response = JsonConvert.DeserializeObject<CapturePaymentResponse>(result.Response);
                }
            }

            return response;
        }

        public GetAllPaymentsResponse GetAllPayments(long? offset = null, long? pageSize = null, string siteOperationId = null, string merchantId = null)
        {
            GetAllPaymentsResponse payments = null;
            string queryString = GetAllPaymentsQuery(offset, pageSize, siteOperationId, merchantId);
            RestResponse result = this.restClient.Get("payments", queryString);

            if (result.StatusCode == STATUS_OK && !String.IsNullOrEmpty(result.Response))
            {
                payments = JsonConvert.DeserializeObject<GetAllPaymentsResponse>(result.Response);
            }
            else
            {
                if (isErrorResponse(result.StatusCode))
                    throw new ResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorResponse>(result.Response));
                else
                    throw new ResponseException(result.StatusCode + " - " + result.Response);
            }

            return payments;
        }

        public PaymentResponse GetPaymentInfo(long paymentId)
        {
            PaymentResponse payment = null;

            string parameter = String.Format("/{0}", paymentId.ToString());

            RestResponse result = this.restClient.Get("payments", String.Format("/{0}?expand=card_data", paymentId.ToString()));

            if (result.StatusCode == STATUS_OK && !String.IsNullOrEmpty(result.Response))
            {
                payment = JsonConvert.DeserializeObject<PaymentResponseExtend>(result.Response);
            }
            else
            {
                if (isErrorResponse(result.StatusCode))
                    throw new ResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorResponse>(result.Response));
                else
                    throw new ResponseException(result.StatusCode + " - " + result.Response);
            }

            return payment;
        }

        public RefundResponse Refund(long paymentId)
        {
            RefundResponse refund = null;
            RestResponse result = this.restClient.Post(String.Format("payments/{0}/refunds", paymentId.ToString()), "{}");

            if (result.StatusCode == STATUS_CREATED && !String.IsNullOrEmpty(result.Response))
            {
                refund = JsonConvert.DeserializeObject<RefundResponse>(result.Response);
            }
            else
            {
                if (isErrorResponse(result.StatusCode))
                    throw new ResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorResponse>(result.Response));
                else
                    throw new ResponseException(result.StatusCode + " - " + result.Response);
            }

            return refund;
        }

        public RefundPaymentResponse RefundSubPayment(long paymentId, String refundSubPaymentRequest)
        {
            RefundPaymentResponse refund = null;


            RestResponse result = this.restClient.Post(String.Format("payments/{0}/refunds", paymentId.ToString()),  refundSubPaymentRequest);
            
            if (result.StatusCode == STATUS_CREATED && !String.IsNullOrEmpty(result.Response))
            {
                refund = JsonConvert.DeserializeObject<RefundPaymentResponse>(result.Response);
            }
            else
            {
                if (isErrorResponse(result.StatusCode))
                    throw new ResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorResponse>(result.Response));
                else
                    throw new ResponseException(result.StatusCode + " - " + result.Response);
            }

            return refund;

        }

        public DeleteRefundResponse DeleteRefund(long paymentId, long refundId)
        {
            DeleteRefundResponse refund = null;
            RestResponse result = this.restClient.Delete(String.Format("payments/{0}/refunds/{1}", paymentId.ToString(), refundId.ToString()));

            if (result.StatusCode == STATUS_OK && !String.IsNullOrEmpty(result.Response))
            {
                refund = JsonConvert.DeserializeObject<DeleteRefundResponse>(result.Response);
            }
            else
            {
                if (isErrorResponse(result.StatusCode))
                    throw new ResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorResponse>(result.Response));
                else
                    throw new ResponseException(result.StatusCode + " - " + result.Response);
            }

            return refund;
        }

        public RefundResponse PartialRefund(long paymentId, double amount)
        {
            RefundResponse refund = null;
            PartialRefund partialRefund = new PartialRefund();

            try
            {
                partialRefund.amount = Convert.ToInt64(amount * 100);
            }
            catch (Exception ex)
            {
                throw new ResponseException(ex.Message);
            }


            RestResponse result = this.restClient.Post(String.Format("payments/{0}/refunds", paymentId.ToString()), Model.PartialRefund.toJson(partialRefund));

            if (result.StatusCode == STATUS_CREATED && !String.IsNullOrEmpty(result.Response))
            {
                refund = JsonConvert.DeserializeObject<RefundResponse>(result.Response);
            }
            else
            {
                if (isErrorResponse(result.StatusCode))
                    throw new ResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorResponse>(result.Response));
                else
                    throw new ResponseException(result.StatusCode + " - " + result.Response);
            }

            return refund;
        }

        public DeleteRefundResponse DeletePartialRefund(long paymentId, long refundId)
        {
            return DeleteRefund(paymentId, refundId);
        }

        protected PaymentResponse sendInstructionThreeDS(string xConsumerUsername, Instruction3dsData instruction3DsData)
        {
            PaymentResponse response = null;
            Model3dsResponse model3ds = null;

            this.headers.Add("X-Consumer-Username", xConsumerUsername);
            this.restClient = new RestClient(this.endpoint, headers, CONTENT_TYPE_APP_JSON);
            RestResponse result = this.restClient.Post("threeds/instruction", toJson(instruction3DsData));

            Console.WriteLine("RESULTADO DE INSTRUCTIONS: " + result.StatusCode + " " + result.Response);

            if (!String.IsNullOrEmpty(result.Response))
            {
                try
                {
                    response = JsonConvert.DeserializeObject<PaymentResponse>(result.Response);
                    if (response.status == STATUS_CHALLENGE_PENDING
                              || response.status == STATUS_FINGERPRINT_PENDING)
                    {
                        model3ds = JsonConvert.DeserializeObject<Model3dsResponse>
                        (result.Response);
                    }
                }
                catch (JsonReaderException j)
                {
                    Console.WriteLine("ERROR DE CASTEO: " + j.ToString());
                    ErrorResponse ErrorPaymentResponse = new ErrorResponse();
                    ErrorPaymentResponse.code = "502";
                    ErrorPaymentResponse.error_type = "Error en recepción de mensaje";
                    ErrorPaymentResponse.message = "No se pudo leer la respuesta";
                    ErrorPaymentResponse.validation_errors = null;
                    throw new PaymentResponseException(ErrorPaymentResponse.code, ErrorPaymentResponse);
                }
            }

            if (response != null)
            {
                response.statusCode = result.StatusCode;
            }
            if (result.StatusCode == STATUS_ACCEPTED)
            {
                throw new PaymentAuth3dsResponseException(result.StatusCode + " - " + result.Response, model3ds, result.StatusCode);
            } 
            else if (result.StatusCode != STATUS_CREATED)
            {
                if (isErrorResponse(result.StatusCode))
                    throw new PaymentResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorResponse>(result.Response), result.StatusCode);
                else
                    throw new PaymentResponseException(result.StatusCode + " - " + result.Response, response, result.StatusCode);
            }

            return response;
        }

            protected PaymentResponse DoPayment(Payment paymentCopy)
        {
            PaymentResponse response = null;
            Model3dsResponse model3ds = null;

            paymentCopy.ConvertDecidirAmounts();

            RestResponse result = this.restClient.Post("payments", Payment.toJson(paymentCopy));

            Console.WriteLine("RESULTADO DE PAYMENT: " + result.StatusCode + " " + result.Response);

            if (!String.IsNullOrEmpty(result.Response))
            {
                    try
                {
                    if (paymentCopy.cardholder_auth_required)
                    {
                        
                        response = JsonConvert.DeserializeObject<PaymentResponse>
                            (result.Response);

                        if (response.status == STATUS_CHALLENGE_PENDING
                               || response.status == STATUS_FINGERPRINT_PENDING)
                        {
                            model3ds = JsonConvert.DeserializeObject<Model3dsResponse>
                            (result.Response);
                        }

                       /*     if (response3ds != null)
                        {
                            if (response3ds.status == STATUS_CHALLENGE_PENDING 
                                || response3ds.status == STATUS_FINGERPRINT_PENDING)
                            {
                                model3ds = new Model3dsResponse();
                                model3ds.id = response3ds.id;
                                model3ds.status = response3ds.status;
                                model3ds.http = response3ds.http;
                                model3ds.timeout = response3ds.timeout;
                                model3ds.target = response3ds.target;

                            }
                        }*/
                        /*response = JsonConvert.DeserializeObject<PaymentAuth3dsResponse>(result.Response);*/
                        Console.WriteLine("RESULTADO DE PAYMENT CON 3DS: " + toJson(result.Response));
                    }
                    else
                    {
                        response = JsonConvert.DeserializeObject<PaymentResponse>(result.Response);

                    }
                    /*Console.WriteLine(toJson(response));*/
                }
                catch (JsonReaderException j)
                {
                    Console.WriteLine("ERROR DE CASTEO: " + j.ToString());
                    ErrorResponse ErrorPaymentResponse = new ErrorResponse();
                    ErrorPaymentResponse.code = "502";
                    ErrorPaymentResponse.error_type = "Error en recepción de mensaje";
                    ErrorPaymentResponse.message = "No se pudo leer la respuesta";
                    ErrorPaymentResponse.validation_errors = null;
                    throw new PaymentResponseException(ErrorPaymentResponse.code, ErrorPaymentResponse );
                }
            }
            if (response != null) { 
            
                response.statusCode = result.StatusCode;
            } 

            if (result.StatusCode != STATUS_CREATED)
            {
                if (result.StatusCode == STATUS_ERROR)
                {
                    throw new PaymentResponseException(result.StatusCode + " - " + result.Response, JsonConvert.DeserializeObject<ErrorResponse>(result.Response), result.StatusCode);
                } else
                {

                    if (isErrorResponse(result.StatusCode))
                    {

                        throw new PaymentResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorResponse>(result.Response), result.StatusCode);
                }else
                    {
                        if (paymentCopy.cardholder_auth_required)
                        {
                            if (result.StatusCode == STATUS_ACCEPTED)
                            {
                                throw new PaymentAuth3dsResponseException(result.StatusCode + " - " + result.Response, model3ds, result.StatusCode);
                            } else
                            {
                                throw new PaymentResponseException(result.StatusCode + " - " + result.Response, response, result.StatusCode);
                            }
                        }

                           /* if (result.StatusCode == STATUS_ACCEPTED && paymentCopy.cardholder_auth_required)
                        {
                            throw new PaymentAuth3dsResponseException(result.StatusCode + " - " + result.Response, model3ds, result.StatusCode);
                        } else
                        {
                            if (paymentCopy.cardholder_auth_required)
                            {
                                throw new PaymentResponseException(result.StatusCode + " - " + result.Response, response3ds, result.StatusCode);
                            } else
                            {
                                throw new PaymentResponseException(result.StatusCode + " - " + result.Response, response,result.StatusCode);

                            }

                        }*/
                    }

                }
            }
            

            return response;
        }

        public static string toJson(Object payment)
        {
            return JsonConvert.SerializeObject(payment, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        private string GetAllPaymentsQuery(long? offset, long? pageSize, string siteOperationId, string merchantId)
        {
            StringBuilder result = new StringBuilder();
            bool isNotNull = false;
            result.Append("?");

            if (offset != null)
            {
                isNotNull = true;
                result.Append(string.Format("{0}={1}", "offset", offset));
            }

            if (pageSize != null)
            {
                isNotNull = true;
                result.Append(string.Format("{0}={1}", "pageSize", pageSize));
            }

            if (!String.IsNullOrEmpty(siteOperationId))
            {
                isNotNull = true;
                result.Append(string.Format("{0}={1}", "siteOperationId", siteOperationId));
            }

            if (!String.IsNullOrEmpty(merchantId))
            {
                isNotNull = true;
                result.Append(string.Format("{0}={1}", "merchantId", merchantId));
            }

            if (isNotNull)
                return result.ToString();

            return String.Empty;
        }


        public ValidateResponse DoValidate(ValidateData validatePayment)
        {
            ValidateResponse response = null;

            this.headers["apikey"]= this.validateApiKey;
            this.headers.Add("X-Consumer-Username", this.merchant);

            this.restClientValidate = new RestClient(this.request_host + "/web/", headers, CONTENT_TYPE_APP_JSON);

            RestResponse result = this.restClientValidate.Post("validate", ValidateData.toJson(validatePayment));

            if (!String.IsNullOrEmpty(result.Response))
            {
                response = JsonConvert.DeserializeObject<ValidateResponse>(result.Response);
            }

            response.statusCode = result.StatusCode;

            if (result.StatusCode != STATUS_CREATED)
            {
                if (isErrorResponse(result.StatusCode))
                    throw new ValidateResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorResponse>(result.Response));
                else
                    throw new ValidateResponseException(result.StatusCode + " - " + result.Response, response);
            }

            return response;
        }


        public ValidateResponse ValidatePayment(ValidateData validateData)
        {
            return DoValidate(validateData);
        }

        public GetTokenResponse GetTokenByCardTokenBsa(CardTokenBsa card_token)
        {
            string cardTokenJson = CardTokenBsa.toJson(card_token);
            return DoGetToken(cardTokenJson);
        }

        public GetTokenResponse GetToken(TokenRequest token)
        {
            string cardTokenJson = TokenRequest.toJson(token);
            return DoGetToken(cardTokenJson);
        }

        private GetTokenResponse DoGetToken(string cardTokenJson)
        {
            GetTokenResponse response = null;

            this.headers["apikey"] = this.publicApiKey;

            this.restClientGetTokenBSA = new RestClient(this.endpoint, this.headers, CONTENT_TYPE_APP_JSON);
            RestResponse result = this.restClientGetTokenBSA.Post("tokens", cardTokenJson);

            if (!String.IsNullOrEmpty(result.Response))
            {
                response = JsonConvert.DeserializeObject<GetTokenResponse>(result.Response);
            }

            if (result.StatusCode != STATUS_CREATED)
            {
                if (isErrorResponse(result.StatusCode))
                    throw new GetTokenResponseException(result.StatusCode.ToString(), JsonConvert.DeserializeObject<ErrorResponse>(result.Response));
                else
                    throw new GetTokenResponseException(result.StatusCode + " - " + result.Response, response);
            }

            return response;

        }
    }
}
