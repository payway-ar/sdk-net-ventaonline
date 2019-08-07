using System.Collections.Generic;

namespace Decidir.Model.CyberSource
{
    public abstract class FraudDetection
    {
        public bool send_to_cs { get; set; }
        public string channel { get; set; }
        public string dispatch_method { get; set; }
        public Address bill_to { get; set; }
        public PurchaseTotals purchase_totals { get; set; }
        public Customer customer_in_site { get; set; }
        public List<Csmdds> csmdds { get; set; }
    }
}
