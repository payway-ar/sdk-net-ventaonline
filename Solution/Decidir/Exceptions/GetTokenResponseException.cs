using System;
using System.Runtime.Serialization;
using Decidir.Model;

namespace Decidir.Exceptions
{
    public class GetTokenResponseException : Exception
    {
        private string v;
        private ErrorResponse errorResponse;
        protected GetTokenResponse getTokenResponse;

        public GetTokenResponseException()
        {
        }

        public GetTokenResponseException(string message) : base(message)
        {
        }

        public GetTokenResponseException(String message, GetTokenResponse getTokenResponse) : base(message)
        {
            this.getTokenResponse = getTokenResponse;
        }


        public GetTokenResponseException(string v, ErrorResponse errorResponse)
        {
            this.v = v;
            this.errorResponse = errorResponse;
        }

        public GetTokenResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GetTokenResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}