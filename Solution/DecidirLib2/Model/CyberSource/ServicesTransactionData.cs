using System.Collections.Generic;

namespace Decidir.Model.CyberSource
{
    public class ServicesTransactionData
    {
        public string delivery_type { get; set; }
        public string reference_payment_service1 { get; set; }
        public string reference_payment_service2 { get; set; }
        public string reference_payment_service3 { get; set; }
        public List<CSItem> items { get; set; }

        public ServicesTransactionData()
        {
            this.items = new List<CSItem>();
        }
    }
}