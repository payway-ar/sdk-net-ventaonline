using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model
{
    public class GetAllPaymentsResponse
    {
        public long limit { get; set; }
        public long offset { get; set; }
        public List<PaymentResponse> results { get; set; }
        public bool hasMore { get; set; }

        public GetAllPaymentsResponse()
        {
            this.results = new List<PaymentResponse>();
        }
    }
}
