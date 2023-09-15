using System.Collections.Generic;

namespace Decidir.Model
{
    public class ErrorInternalTokenResponse
    {
        public string title { get; set; }
        public List<DescriptionError> details { get; set; }
        public string description { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        public string trace_id { get; set; }
        public string span_id { get; set; }
    }
}
