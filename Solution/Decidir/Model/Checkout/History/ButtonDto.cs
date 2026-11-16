using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model
{
    public class ButtonDto
    {
        public string site_id { get; set; }
        public string operation_id { get; set; }
        public long? payment_method_id { get; set; }
        public double? amount { get; set; }
        public int? installments { get; set; }
        public string link_hash { get; set; }
        public string created_at { get; set; }
        public string link_expires_at { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string link_updated_at { get; set; }
        public long? establishment_number { get; set; }
        public int? send_to_cs { get; set; }
        public string source_app { get; set; }
        public string platform { get; set; }
        public string holder_name { get; set; }
        public string auth_code { get; set; }
        public string card_number { get; set; }
        public string cuit { get; set; }
        public string currency { get; set; }
        public string charge_id { get; set; }
        public string site_transaction_id { get; set; }
        public string installment_type { get; set; }
        public string url { get; set; }
    }
}