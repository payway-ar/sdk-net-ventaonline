using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model
{
    public class CardError
    {
        public string type { get; set; }
        private CardErrorReason reason;
    }
}
