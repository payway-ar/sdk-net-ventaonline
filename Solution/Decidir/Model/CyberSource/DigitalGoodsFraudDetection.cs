using System.Collections.Generic;

namespace Decidir.Model.CyberSource
{
    public class DigitalGoodsFraudDetection : FraudDetection
    {
        public DigitalGoodsTransactionData digital_goods_transaction_data { get; set; }
        public string device_unique_id { get; set; }

        public DigitalGoodsFraudDetection()
        {
            this.send_to_cs = true;
            this.bill_to = new Address();
            this.purchase_totals = new PurchaseTotals();
            this.customer_in_site = new Customer();
            this.csmdds = new List<Csmdds>();
            this.digital_goods_transaction_data = new DigitalGoodsTransactionData();
        }
    }
}
