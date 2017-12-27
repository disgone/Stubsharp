using System.Collections.Generic;

namespace Stubsharp.Models.Response
{
    public class ErrorResponse
    {
        public Errors Errors { get; set; }
    }

    public class Errors
    {
        public IReadOnlyCollection<Error> Error { get; set; } = new List<Error>();
    }

    public class Error
    {
        public string ErrorType { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorParam { get; set; }
        public string ErrorTypeId { get; set; }
    }
}
