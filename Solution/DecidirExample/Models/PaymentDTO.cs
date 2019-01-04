using System.Collections.Generic;

namespace DecidirExample.Models
{
    public class PaymentDTO
    {
        public int AmbienteId { get; set; }

        public string privateApiKey { get; set; }
        public string publicApiKey { get; set; }

        public string site_transaction_id { get; set; }
        public string token { get; set; }
        public string email { get; set; }
        public string user_id { get; set; }
        public int payment_method_id { get; set; }
        public string bin { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public long installments { get; set; }
        public string description { get; set; }
        public string payment_type { get; set; }
        public string establishment_name { get; set; }
        public List<object> sub_payments { get; set; }

        public PaymentDTO()
        {
            this.sub_payments = new List<object>();
        }
    }
}