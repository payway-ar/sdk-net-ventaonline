using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Decidir.Model;


namespace Decidir.Exceptions
{
    public class ValidateResponseException : ResponseException
    {
        protected ValidateResponse validateResponse;
        public ValidateResponseException(String message) : base(message)
        {
        }
        public ValidateResponseException(String message, ValidateResponse validateResponse) : base(message)
        {
            this.validateResponse = validateResponse;
        }

        public ValidateResponseException(String message, ErrorResponse errorResponse) : base(message, errorResponse)
        {
            this.errorResponse = errorResponse;
        }

        public ValidateResponse GetValidateResponse()
        {
            return this.validateResponse;
        }
    }
}
