using Decidir.Model;
using System;


namespace Decidir.Exceptions
{
    public class PaymentResponseException : ResponseException
    {
        protected PaymentResponse paymentResponse;

        public PaymentResponseException(String message) : base(message)
        {
        }

        public PaymentResponseException(String message, PaymentResponse paymentResponse) : base(message)
        {
            this.paymentResponse = paymentResponse;
        }

        public PaymentResponse GetPaymentResponse()
        {
            return this.paymentResponse;
        }
    }
}
