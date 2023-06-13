using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model
{
    public class RefundPaymentResponse : RefundResponse
    {
        
        public List<RefundSubPaymentResponse> sub_payments { get; set; }

    }
}
