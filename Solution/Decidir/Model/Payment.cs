using Newtonsoft.Json;
using System.Collections.Generic;
using Decidir.Model.CyberSource;
using System;
using Decidir.Exceptions;

namespace Decidir.Model
{
    public class Payment
    {
        public string site_transaction_id { get; set; }
        public string token { get; set; }
        public CustomerData customer { get; set; }
        public int payment_method_id { get; set; }
        public string bin { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public long installments { get; set; }
        public string description { get; set; }
        public string payment_type { get; set; }
        public string establishment_name { get; set; }
        public List<object> sub_payments { get; set; }
        public FraudDetection fraud_detection { get; set; }
        public string site_id { get; set; }
        public AggregateDataPayment aggregate_data { get; set; }
        public CardTokenBsa card_token_bsa { get; set; }

        public Payment()
        {
            this.sub_payments = new List<object>();
            this.customer = new CustomerData();
            this.site_id = null;
        }

        public static string toJson(Payment payment)
        {
            return JsonConvert.SerializeObject(payment, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        internal Payment copy()
        {
            Payment payment = new Payment();

            payment.site_transaction_id = this.site_transaction_id;
            payment.token = this.token;
            payment.customer = this.customer;
            payment.payment_method_id = this.payment_method_id;
            payment.bin = this.bin;
            payment.amount = this.amount;
            payment.currency = this.currency;
            payment.installments = this.installments;
            payment.description = this.description;
            payment.payment_type = this.payment_type;
            payment.establishment_name = this.establishment_name;
            payment.fraud_detection = this.fraud_detection;
            payment.site_id = this.site_id;
            payment.aggregate_data = this.aggregate_data;

            foreach (object o in this.sub_payments)
                payment.sub_payments.Add(o);

            return payment;
        }

        public virtual void ConvertDecidirAmounts()
        {
            try
            {
                this.amount = Convert.ToInt64(this.amount * 100);

                foreach (object o in this.sub_payments)
                    ((SubPayment)o).amount = Convert.ToInt64(((SubPayment)o).amount * 100);

            }
            catch (Exception ex)
            {
                throw new ResponseException(ex.Message);
            }
        }
    }
}
