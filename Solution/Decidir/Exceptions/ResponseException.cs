using System;

namespace Decidir.Exceptions
{
    public class ResponseException : Exception
    {
        public ResponseException(String message) : base(message)
        {
        }
    }
}
