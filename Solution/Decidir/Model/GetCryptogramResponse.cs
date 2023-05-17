using System.Collections.Generic;
using Decidir.Model.CyberSource;

namespace Decidir.Model
{
    public class GetCryptogramResponse
    {
        public string charge_id { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public string status { get; set; }
        public StatusDetails status_details { get; set; }
        public string date { get; set; }
        public string installments { get; set; }
        public string first_installment_expiration_date { get; set; }
        public List<object> sub_payments { get; set; }
        public FraudDetectionCryptogram fraud_detection { get; set; }
        public string tid { get; set; }
        public string trace_id { get; set; }
        public string span_id { get; set; }


    }
}