using System.Net;
using Azure;
using Linkdev.Talabat.APIs.Controllers.Errors;
using Linkdev.Talabat.Core.Application.Exceptions;

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
                //if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                //{
                //    await httpContext.Response.WriteAsync(new ApiResponse((int)HttpStatusCode.NotFound, $"The requested Endpoint:{httpContext.Request.Path}").ToString());
                //}

            }
            catch (Exception ex)
            {
                #region Logging

                if (_enviroment.IsDevelopment())
                {
                    // Development mode
                    _logger.LogError(ex.Message, ex);
                }
                else
                {
                    // Production mode
                    // logError =-=-> Database / External file (text/json)
                }

                #endregion

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ApiResponse response;

            switch (ex)
            {
                case NotFoundException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = new ApiResponse(404, ex.Message);
                    httpContext.Response.ContentType = "Application/json";
                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                case ValidationErrorException validationErrorException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ApiValidationErrorResponse(400, ex.Message) { Errors = validationErrorException.Errors };
                    httpContext.Response.ContentType = "Application/json";
                    await httpContext.Response.WriteAsync(response.ToString());
                    break;
                case BadRequestException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ApiResponse(400, ex.Message);
                    httpContext.Response.ContentType = "Application/json";
                    await httpContext.Response.WriteAsync(response.ToString());
                    break;
                case UnAuthorizedException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = new ApiResponse(401, ex.Message);
                    httpContext.Response.ContentType = "Application/json";
                    await httpContext.Response.WriteAsync(response.ToString());
                    break;
                default:

                    // Development mode
                    response = _enviroment.IsDevelopment() ?
                        new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString()) :
                        new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                    
                    httpContext.Response.ContentType = "Application/json";
                    await httpContext.Response.WriteAsync(response.ToString());

                    break;
            }
        }
    }
}
