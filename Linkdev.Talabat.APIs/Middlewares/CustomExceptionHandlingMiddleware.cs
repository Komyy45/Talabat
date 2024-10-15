using System.Net;
using Linkdev.Talabat.APIs.Controllers.Errors;
using Linkdev.Talabat.APIs.Exceptions;

namespace Linkdev.Talabat.APIs.Middlewares
{
    public class CustomExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _enviroment;
        private readonly ILogger<CustomExceptionHandlingMiddleware> _logger;

        public CustomExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment enviroment, ILogger<CustomExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _enviroment = enviroment;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                // logic Executed before the Request

                await _next(httpContext);

                // logic Executed before the Response
            }
            catch (Exception ex)
            {
                ApiResponse response;

                switch(ex)
                {
                    case NotFoundException:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        response = new ApiResponse(404, ex.Message);
                        httpContext.Response.ContentType = "Application/json";
                        await httpContext.Response.WriteAsync(response.ToString());
                        break;

                    default:

                        // Development mode
                        if (_enviroment.IsDevelopment())
                        {
                            _logger.LogError(ex.Message, ex);
                            response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString());
                        }
                        else
                        {
                            // Production mode
                            // logError =-=-> Database / External file

                            response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                        }

                        httpContext.Response.ContentType = "Application/json";
                        await httpContext.Response.WriteAsync(response.ToString());


                        break;
                }



            }
        }
    }
}
