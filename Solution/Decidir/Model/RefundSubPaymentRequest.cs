using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Decidir.Model
{
    public class RefundSubPaymentRequest
    {

        public List<RefundSubPayment> sub_payments = new List<RefundSubPayment>();

        public List<RefundSubPayment> Sub_payments
        {
            get { return sub_payments; }
            set { sub_payments = value; }
        }


        public static string toJson(RefundSubPaymentRequest refundSubPaymentRequest)
        {
            return JsonConvert.SerializeObject(refundSubPaymentRequest, Newtonsoft.Json.Formatting.None);
        }

    }
}
