using System.Collections.Generic;

namespace Decidir.Model.CyberSource
{
    public class ServicesFraudDetection : FraudDetection
    {
        public ServicesTransactionData services_transaction_data { get; set; }

        public ServicesFraudDetection()
        {
            this.send_to_cs = true;
            this.bill_to = new Address();
            this.purchase_totals = new PurchaseTotals();
            this.customer_in_site = new Customer();
            this.csmdds = new List<Csmdds>();
            this.services_transaction_data = new ServicesTransactionData();
        }
    }
}
