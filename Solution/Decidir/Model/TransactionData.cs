using System.Collections.Generic;

namespace Decidir.Model

{
    public class TransactionData
    {
        public string merchant_transaction_id { get; set; }
        public string payment_method_id { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string installments { get; set; }
        public string payment_type { get; set; }
        public List<object> sub_payments { get; set; }
        public string description { get; set; }

    }
}