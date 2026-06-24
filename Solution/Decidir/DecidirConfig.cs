using Decidir.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decidir
{
    public class DecidirConfig
    {
        public EAmbiente Ambiente { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string SiteId { get; set; }
        public string PurchaseLegend { get; set; }

        public bool UseSandbox { get; set; }
        public int AmbienteId
        {
            get
            {
                return Ambiente.GetAmbiente();
            }
        }
    }
}
