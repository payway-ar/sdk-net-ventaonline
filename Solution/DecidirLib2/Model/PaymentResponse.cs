using Newtonsoft.Json;
using System.Collections.Generic;

namespace Decidir.Model
{
    public class PaymentResponse
    {
        public long id { get; set; }
        public string site_transaction_id { get; set; }
        public string token { get; set; }
        public string user_id { get; set; }
        public int? payment_method_id { get; set; }
        public string bin { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public int? installments { get; set; }
        public string description { get; set; }
        public string payment_type { get; set; }
        public string establishment_name { get; set; }
        public List<SubPaymentResponse> sub_payments { get; set; }
        public string status { get; set; }
        public StatusDetails status_details { get; set; }
        public string date { get; set; }
        public string merchant_id { get; set; }
        public Dictionary<string, object> fraud_detection { get; set; }
        public int statusCode { get; set; }

        public string site_id { get; set; }
        public string pan { get; set; }
        public Dictionary<string, string> aggregate_data { get; set; }

        public CustomerData customer { get; set; }
        public string card_brand { get; set; }
        public string first_installment_expiration_date { get; set; }

        public string spv { get; set; }
        public string confirmed { get; set; }
        public string card_data { get; set; }

        public string customer_token { get; set; }

        public PaymentResponse()
        {
            this.sub_payments = new List<SubPaymentResponse>();
            this.fraud_detection = new Dictionary<string, object>();
            this.aggregate_data = new Dictionary<string, string>();
            this.status_details = new StatusDetails();
        }
    }

    public class PaymentResponseExtend : PaymentResponse
    {
        [JsonProperty(PropertyName = "card_data")]
        public CardDataPayment cardData { get; set; }
    }

}
