using System.Collections.Generic;

namespace Decidir.Model
{
    public class ExternalPaymentData
    {
        public string integration_type { get; set; }
        public string signature { get; set; }
        public IntermediateSigningKey intermediate_signing_key { get; set; }
        public string protocol_version { get; set; }
        public string signed_message { get; set; }
    }

    public class IntermediateSigningKey
    {
        public string signed_key { get; set; }
        public List<string> signatures { get; set; }
    }
}