using System.Collections.Generic;

namespace Decidir.Model
{
    public class ValidatePayment
    {
        public double amount { get; set; }
        public string currency { get; set; }
        public int payment_method_id { get; set; }
        public string bin { get; set; }
        public long installments { get; set; }
        public string payment_type { get; set; }
        public List<object> sub_payments { get; set; }
    }
}