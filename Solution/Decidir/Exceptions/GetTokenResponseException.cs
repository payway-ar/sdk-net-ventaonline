using System;
using System.Runtime.Serialization;
using Decidir.Model;

namespace Decidir.Exceptions
{
    public class GetTokenResponseException : Exception
    {
        private string v;
        public ErrorResponse errorResponse { get; set; }

        protected GetTokenResponse getTokenResponse;
        public ErrorInternalTokenResponse getInternalTokenResponse { get; set; }
        public int statusCode { get; set; }

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

        public GetTokenResponseException(String message, ErrorInternalTokenResponse getTokenResponse, int statusCode) : base(message)
        {
            this.getInternalTokenResponse = getTokenResponse;
            this.statusCode = statusCode;
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