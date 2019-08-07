using System.Collections.Generic;

namespace Decidir.Model.CyberSource
{
    public class RetailFraudDetection : FraudDetection
    {
        public RetailTransactionData retail_transaction_data { get; set; }

        public RetailFraudDetection()
        {
            this.send_to_cs = true;
            this.bill_to = new Address();
            this.purchase_totals = new PurchaseTotals();
            this.customer_in_site = new Customer();
            this.csmdds = new List<Csmdds>();
            this.retail_transaction_data = new RetailTransactionData();
        }
    }
}
