using System;
using Decidir.Model;

namespace Decidir.Exceptions
{
    public class CheckoutResponseException : Exception
    {
        protected ErrorCheckoutResponse errorCheckoutResponse { get; set; }
        protected int statusCode;

        public CheckoutResponseException(String message, ErrorCheckoutResponse paymentResponse, int statuscode) : base(message)
        {
            this.errorCheckoutResponse = paymentResponse;
            this.statusCode = statuscode;
        }

        public ErrorCheckoutResponse getErrorCheckoutResponse()
        {
            return this.errorCheckoutResponse;
        }

        public int GetStatusCode()
        {
            return this.statusCode;
        }
    }
}
