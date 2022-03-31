using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model
{
    public class RefundPaymentResponse
    {
        public long id { get; set; }
        public long amount { get; set; }
        private CardError error { get; set; }
        public List<RefundSubPaymentResponse> sub_payments { get; set; }

        public string status { get; set; }  

        public StatusDetails status_details { get; set; }
    }
}
