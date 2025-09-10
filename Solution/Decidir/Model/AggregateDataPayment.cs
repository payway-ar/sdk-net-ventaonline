namespace Decidir.Model
{
    public class AggregateDataPayment
    {
        public string indicator { get; set; }
        public string identification_number { get; set; }
        public string bill_to_pay { get; set; }
        public string bill_to_refund { get; set; }
        public string merchant_name { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string postal_code { get; set; }
        public string category { get; set; }
        public string channel { get; set; }
        public string geographic_code { get; set; }
        public string city { get; set; }
        public string merchant_id { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string product { get; set; }
        public string origin_country { get; set; }
        public string merchant_url { get; set; }
        public string aggregator_name { get; set; }
        public string gateway_id { get; set; }
        public bool enabled { get; set; }
        public string merchant_email { get; set; }
        public string merchant_phone { get; set; }

    }
}