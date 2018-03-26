using Decidir.Model;
using System;

namespace Decidir.Exceptions
{
    public class ResponseException : Exception
    {
        protected ErrorResponse errorResponse;

        public ResponseException(String message) : base(message)
        {
        }

        public ResponseException(String message, ErrorResponse errorResponse) : base(message)
        {
            this.errorResponse = errorResponse;
        }

        public ErrorResponse GetErrorResponse()
        {
            return this.errorResponse;
        }
    }
}
