using System.Collections.Generic;

namespace Decidir.Model.CyberSource
{
    public class DigitalGoodsTransactionData
    {
        public string delivery_type { get; set; }
        public List<CSItem> items { get; set; }

        public DigitalGoodsTransactionData()
        {
            this.items = new List<CSItem>();
        }
    }
}