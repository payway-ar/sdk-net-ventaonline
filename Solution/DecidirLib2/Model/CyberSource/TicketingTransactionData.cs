using System.Collections.Generic;

namespace Decidir.Model.CyberSource
{
    public class TicketingTransactionData
    {
        public int days_to_event { get; set; }
        public string delivery_type { get; set; }
        public List<CSItem> items { get; set; }

        public TicketingTransactionData()
        {
            this.items = new List<CSItem>();
        }
    }
}
