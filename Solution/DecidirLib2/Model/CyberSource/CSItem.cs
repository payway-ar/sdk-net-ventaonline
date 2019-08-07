namespace Decidir.Model.CyberSource
{
    public class CSItem
    {
        public string code { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string sku { get; set; }
        public long total_amount { get; set; }
        public int quantity { get; set; }
        public long unit_price { get; set; }
    }
}