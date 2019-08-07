using System.Collections.Generic;

namespace Decidir.Model.CyberSource
{
    public class RetailTransactionData
    {
        public Address ship_to { get; set; }
        public string days_to_delivery { get; set; }
        public bool tax_voucher_required { get; set; }
        public string customer_loyality_number { get; set; }
        public string coupon_code { get; set; }

        public List<CSItem> items { get; set; }

        public RetailTransactionData()
        {
            this.ship_to = new Address();
            this.items = new List<CSItem>();
        }
    }
}