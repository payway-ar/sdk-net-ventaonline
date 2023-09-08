using System;

namespace Decidir.Model
{
    public class CheckoutProductModel
    {
        public long id { get; set; }
        public double volume { get; set; }
        public double weight { get; set; }
        public double value { get; set; }
        public string description { get; set; }
        public int category_id { get; set; }
        public int quantity { get; set; }
    }
}
