using System;
using Decidir.Model;

namespace Decidir.Exceptions
{
    public class CheckoutHistoryException : Exception
    {
        protected CheckoutHistoryError checkoutHistoryError { get; set; }
        protected int statusCode;

        public CheckoutHistoryException(string message, CheckoutHistoryError checkoutHistoryError, int statusCode) : base(message)
        {
            this.checkoutHistoryError = checkoutHistoryError;
            this.statusCode = statusCode;
        }

        public CheckoutHistoryError getCheckoutHistoryError()
        {
            return this.checkoutHistoryError;
        }

        public int GetStatusCode()
        {
            return this.statusCode;
        }
    }
}