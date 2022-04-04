using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model
{
    public class RefundSubPaymentResponse
    {
        public long id { get; set; }
        public long amount { get; set; }
        public long refund_id { get; set; }
        public CardError error { get; set; }

        public string status { get; set; }  


    }
}
