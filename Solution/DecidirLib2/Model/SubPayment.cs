namespace Decidir.Model
{
    public class SubPayment
    {
        public string site_id { get; set; }
        public double amount { get; set; }
        public int? installments { get; set; }
    }
}
