using System.Collections.Generic;

namespace Decidir.Model.CyberSource
{
    public class FraudDetectionStatus
    {
        public string decision {  get; set; }
        public string request_id { get; set; }
        public string reason_code { get; set; }
        public string description { get; set; }
        public Review review { get; set; }
        public ErrorType details { get; set; }
    }
}
