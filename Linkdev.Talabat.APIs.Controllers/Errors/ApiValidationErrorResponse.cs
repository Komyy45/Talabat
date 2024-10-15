namespace Linkdev.Talabat.APIs.Controllers.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public required IDictionary<string, IEnumerable<string>> Errors { get; set; }

        public ApiValidationErrorResponse(int statusCode, string? message = null) 
            : base(statusCode, message)
        {
            
        }
    }
}
