namespace Linkdev.Talabat.APIs.Controllers.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode();
        }

        private string? GetDefaultMessageForStatusCode()
        => StatusCode switch
        {
            400 => "BadRequest",
            401 => "Unauthorized",
            404 => "NotFound",
            500 => "Errors are path to the dark side. Errors lead to anger. Anger lead to hate. Hate lead to carrer change",
            _ => null
        };
    }
}
