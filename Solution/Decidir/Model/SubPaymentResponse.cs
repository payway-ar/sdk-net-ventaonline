namespace Decidir.Model
{
    public class SubPaymentResponse
    {
        public string site_id { get; set; }
        public int amount { get; set; }
        public int? installments { get; set; }
        public string ticket { get; set; }
        public string card_authorization_code { get; set; }
        public long? subpayment_id { get; set; }
        public string status { get; set; }
    }
}
