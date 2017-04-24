using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model.CyberSource
{
    public class TicketingFraudDetection : FraudDetection
    {
        public TicketingTransactionData ticketing_transaction_data { get; set; }

        public TicketingFraudDetection()
        {
            this.send_to_cs = true;
            this.bill_to = new Address();
            this.purchase_totals = new PurchaseTotals();
            this.customer_in_site = new Customer();
            this.csmdds = new List<Csmdds>();
            this.ticketing_transaction_data = new TicketingTransactionData();
        }
    }
}
