using System.Collections.Generic;

public class ErrorType
{
    public string error_type {  get; set; }
    public List<ValidationError> validation_errors { get; set; }
}
