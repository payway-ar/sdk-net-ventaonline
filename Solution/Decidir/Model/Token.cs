namespace Decidir.Model
{
    public class Token
    {
        public string token { get; set; }
        public int payment_method_id { get; set; }
        public string bin { get; set; }
        public string last_four_digits { get; set; }
        public string expiration_month { get; set; }
        public string expiration_year { get; set; }
        public string expired { get; set; }
    }
}
