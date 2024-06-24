using System;

namespace Decidir.Model
{
    public class CheckoutResponse
    {
        public CheckoutGenerateHashResponse response { get; set; }
        public int statusCode { get; set; }
    }
}