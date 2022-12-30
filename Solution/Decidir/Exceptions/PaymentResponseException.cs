using Decidir.Model;
using System;

namespace Decidir.Exceptions
{
    public class PaymentResponseException : ResponseException
    {
        protected PaymentResponse paymentResponse;
        protected int statusCode;
        
        public PaymentResponseException(String message) : base(message)
        {
        }

        public PaymentResponseException(String message, PaymentResponse paymentResponse, int statuscode) : base(message)
        {
            this.paymentResponse = paymentResponse;
            this.statusCode = statuscode;
        }

        public PaymentResponseException(String message, ErrorResponse errorResponse) : base(message, errorResponse)
        {
            this.errorResponse = errorResponse;
        }

        public PaymentResponseException(String message, ErrorResponse errorResponse, int statuscode) : base(message, errorResponse)
        {
            this.errorResponse = errorResponse;
            this.statusCode = statuscode;
        }

        public PaymentResponse GetPaymentResponse()
        {
            return this.paymentResponse;
        }

        public int GetStatusCode()
        {
            return this.statusCode;
        }
    }
}
