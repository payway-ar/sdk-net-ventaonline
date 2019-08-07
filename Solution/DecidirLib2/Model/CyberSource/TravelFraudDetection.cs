using System.Collections.Generic;

namespace Decidir.Model.CyberSource
{
    public class TravelFraudDetection : FraudDetection
    {
        public TravelTransactionData travel_transaction_data { get; set; }
        public string device_unique_id { get; set; }

        public TravelFraudDetection()
        {
            this.send_to_cs = true;
            this.bill_to = new Address();
            this.purchase_totals = new PurchaseTotals();
            this.customer_in_site = new Customer();
            this.csmdds = new List<Csmdds>();
            this.travel_transaction_data = new TravelTransactionData();
        }
    }
}


