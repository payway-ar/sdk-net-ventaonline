using System;
using System.Collections.Generic;
using System.Text;

namespace Decidir.Constants
{
    public class DecidirConnectionSettings : DecidirAbstractConnectionSettings
    {
        public override EAmbiente Environment { get; set; } = EAmbiente.SANDBOX;
    
    }
}
