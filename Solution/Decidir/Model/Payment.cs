using Newtonsoft.Json;
using System.Collections.Generic;
using Decidir.Model.CyberSource;

namespace Decidir.Model
{
    public class Payment
    {
        public string site_transaction_id { get; set; }
        public string token { get; set; }
        public string user_id { get; set; }
        public int payment_method_id { get; set; }
        public string bin { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public long installments { get; set; }
        public string description { get; set; }
        public string payment_type { get; set; }
        public List<object> sub_payments { get; set; }
        public FraudDetection fraud_detection { get; set; }

        public Payment()
        {
            this.sub_payments = new List<object>();
        }

        public static string toJson(Payment payment)
        {
            return JsonConvert.SerializeObject(payment, Newtonsoft.Json.Formatting.None);
        }

        internal Payment copy()
        {
            Payment payment = new Payment();

            payment.site_transaction_id = this.site_transaction_id;
            payment.token = this.token;
            payment.user_id = this.user_id;
            payment.payment_method_id = this.payment_method_id;
            payment.bin = this.bin;
            payment.amount = this.amount;
            payment.currency = this.currency;
            payment.installments = this.installments;
            payment.description = this.description;
            payment.payment_type = this.payment_type;
            payment.fraud_detection = this.fraud_detection;

            foreach (object o in this.sub_payments)
                payment.sub_payments.Add(o);

            return payment;
        }
    }
}
