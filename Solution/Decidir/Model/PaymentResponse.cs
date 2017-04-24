using System.Collections.Generic;

namespace Decidir.Model
{
    public class PaymentResponse
    {
        public long id { get; set; }
        public string site_transaction_id { get; set; }
        public string token { get; set; }
        public string user_id { get; set; }
        public string payment_method_id { get; set; }
        public string bin { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public int installments { get; set; }
        public string description { get; set; }
        public string payment_type { get; set; }
        public List<PaymentResponse> sub_payments { get; set; }
        public string status { get; set; }
        public string status_details { get; set; }
        public string date { get; set; }
        public string merchant_id { get; set; }
        public Dictionary<string, string> fraud_detection { get; set; }

        public PaymentResponse()
        {
            this.sub_payments = new List<PaymentResponse>();
            this.fraud_detection = new Dictionary<string, string>();
        }
    }
}
