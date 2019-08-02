namespace Decidir.Model
{
    public class GetTokenResponse
    {
        public string id { get; set; }
        public string status { get; set; }
        public int card_number_length { get; set; }
        public string date_created { get; set; }
        public string bin { get; set; }
        public string last_four_digits { get; set; }
        public int security_code_length { get; set; }
        public int expiration_month { get; set; }
        public int expiration_year { get; set; }
        public string date_due { get; set; }
        public CardHolder cardholder { get; set; }
    }
}