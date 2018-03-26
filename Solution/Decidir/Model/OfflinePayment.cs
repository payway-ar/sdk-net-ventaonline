namespace Decidir.Model
{
    public class OfflinePayment : Payment
    {
        public string email { get; set; }
        public string invoice_expiration { get; set; }
        public string cod_p3 { get; set; }
        public string cod_p4 { get; set; }
        public string client { get; set; }
        public int surcharge { get; set; }
        public string second_invoice_expiration { get; set; }
        public string payment_mode { get; set; }

        public OfflinePayment() : base()
        {

        }

        internal OfflinePayment copyOffline()
        {
            OfflinePayment payment = new OfflinePayment();

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
            payment.fraud_detection = this.fraud_detection;
            payment.site_id = this.site_id;

            payment.email = this.email;
            payment.invoice_expiration = this.invoice_expiration;
            payment.cod_p3 = this.cod_p3;
            payment.cod_p4 = this.cod_p4;
            payment.client = this.client;
            payment.payment_mode = this.payment_mode;
            payment.second_invoice_expiration = this.second_invoice_expiration;
            payment.surcharge = this.surcharge;

            foreach (object o in this.sub_payments)
                payment.sub_payments.Add(o);

            return payment;
        }
    }
}
