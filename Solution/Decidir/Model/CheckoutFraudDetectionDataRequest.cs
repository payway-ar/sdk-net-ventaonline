using System;
using System.Collections.Generic;
using Decidir.Model.CyberSource;

namespace Decidir.Model
{
    public class CheckoutFraudDetectionDataRequest
    {
        public Address bill_to { get; set; }
        public PurchaseTotals purchase_totals { get; set; }
        public string channel {  get; set; }
        public string dispatch_method { get; set; }
        public Customer customer_in_site { get; set; }
        public Boolean send_to_cs { get; set; }
        public string device_unique_id { get; set; }
        public List<Csmdds> csmdds { get; set; }
        public RetailTransactionData retail_transaction_data { get; set; }

    }

}