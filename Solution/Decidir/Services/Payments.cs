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

        public Payments(string endpoint, string privateApiKey) : base(endpoint)
        {
            this.privateApiKey = privateApiKey;

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("apikey", this.privateApiKey);
            headers.Add("Cache-Control", "no-cache");

            this.restClient = new RestClient(this.endpoint, headers, CONTENT_TYPE_APP_JSON);
        }

        public PaymentResponse ExecutePayment(Payment payment)
        {
            PaymentResponse response = null;
            Payment paymentCopy = payment.copy();

            try
            {
                paymentCopy.amount = Convert.ToInt64(payment.amount * 100);
            }
            catch (Exception ex)
            {
                throw new ResponseException("500 - " + ex.Message);
            }

            RestResponse result = this.restClient.Post("payments", Payment.toJson(paymentCopy));

            if (!String.IsNullOrEmpty(result.Response))
            {
                response = JsonConvert.DeserializeObject<PaymentResponse>(result.Response);
            }

            response.statusCode = result.StatusCode;

            if (result.StatusCode != STATUS_CREATED)
            {
                throw new PaymentResponseException(result.StatusCode.ToString(), response);
            }

            return response;
        }

        public CapturePaymentResponse CapturePayment(long paymentId, double amount)
        {
            int amountCapture = Convert.ToInt32(amount * 100);
            CapturePaymentResponse response = null;
            RestResponse result = this.restClient.Put(String.Format("payments/{0}", paymentId.ToString()), "{\"amount\": " + amountCapture.ToString() + " }");

            if (result.StatusCode != STATUS_NOCONTENT && result.StatusCode != STATUS_OK)
            {
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
                throw new ResponseException(result.StatusCode + " - " + result.Response);
            }

            return payments;
        }

        public PaymentResponse GetPaymentInfo(long paymentId)
        {
            PaymentResponse payment = null;
            RestResponse result = this.restClient.Get("payments", String.Format("/{0}", paymentId.ToString()));

            if (result.StatusCode == STATUS_OK && !String.IsNullOrEmpty(result.Response))
            {
                payment = JsonConvert.DeserializeObject<PaymentResponse>(result.Response);
            }
            else
            {
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
                throw new ResponseException("500 - " + ex.Message);
            }


            RestResponse result = this.restClient.Post(String.Format("payments/{0}/refunds", paymentId.ToString()), Model.PartialRefund.toJson(partialRefund));

            if (result.StatusCode == STATUS_CREATED && !String.IsNullOrEmpty(result.Response))
            {
                refund = JsonConvert.DeserializeObject<RefundResponse>(result.Response);
            }
            else
            {
                throw new ResponseException(result.StatusCode + " - " + result.Response);
            }

            return refund;
        }

        public DeleteRefundResponse DeletePartialRefund(long paymentId, long refundId)
        {
            return DeleteRefund(paymentId, refundId);
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
    }
}
