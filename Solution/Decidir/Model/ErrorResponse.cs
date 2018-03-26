using System.Collections.Generic;

namespace Decidir.Model
{
    public class ErrorResponse
    {
        public string error_type { get; set; }
        public List<ErrorValidationResponse> validation_errors { get; set; }
    }
}
