using System.Collections.Generic;

namespace Decidir.Model
{
    public class CheckoutHistoryError
    {
        public string id { get; set; }
        public string error_type { get; set; }
        public string message { get; set; }
    }
}