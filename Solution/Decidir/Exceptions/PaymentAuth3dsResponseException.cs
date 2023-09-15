using Decidir.Model;
using System;

namespace Decidir.Exceptions
{
    public class PaymentAuth3dsResponseException : ResponseException
    {
        protected PaymentResponse paymentResponse;
        protected Model3dsResponse model3dsResponse;
        protected int statusCode;
        
        public PaymentAuth3dsResponseException(String message) : base(message)
        {
        }

        public PaymentAuth3dsResponseException(String message, Model3dsResponse model3dsResponse, int statuscode) : base(message)
        {
            this.model3dsResponse = model3dsResponse;
            this.statusCode = statuscode;
        }

        public PaymentAuth3dsResponseException(String message, ErrorResponse errorResponse) : base(message, errorResponse)
        {
            this.errorResponse = errorResponse;
        }

        public PaymentAuth3dsResponseException(String message, ErrorResponse errorResponse, int statuscode) : base(message, errorResponse)
        {
            this.errorResponse = errorResponse;
            this.statusCode = statuscode;
        }

        public PaymentResponse GetPaymentResponse()
        {
            return this.paymentResponse;
        }

        public Model3dsResponse GetModel3dsResponse()
        {
            return this.model3dsResponse;
        }

        public int GetStatusCode()
        {
            return this.statusCode;
        }
    }
}
