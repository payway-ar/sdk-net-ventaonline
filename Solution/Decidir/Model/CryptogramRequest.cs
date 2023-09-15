using Decidir.Model;

namespace Decidir
{
    public class CryptogramRequest
    {
        public bool store_credential { get; set; }
        public string merchant_id { get; set; }
        public TransactionData transaction_data { get; set; }
        public CustomerDataCryptogram customer_data { get; set; }

    }
}