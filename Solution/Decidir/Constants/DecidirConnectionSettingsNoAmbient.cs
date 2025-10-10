using System;
using System.Collections.Generic;
using System.Text;

namespace Decidir.Constants
{
    public class DecidirConnectionSettingsNoAmbient : DecidirAbstractConnectionSettings
    {
        public override EAmbiente Environment { get; set; } = EAmbiente.NA;
        public string RequestHost { get; set; }
        public string RequestPath { get; set; }
            
    }
}
