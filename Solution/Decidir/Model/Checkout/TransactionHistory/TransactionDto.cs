using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model
{
    public class TransactionDto
    {
        public string transaction_id { get; set; }
        public string status_id { get; set; }
        public string created_at { get; set; }
        public string status_description { get; set; }
        public string holder_name { get; set; }
        public string card_number { get; set; }
        public string payment_method_id { get; set; }
        public string rejection_description { get; set; }
        public string tid { get; set; }
    }
}