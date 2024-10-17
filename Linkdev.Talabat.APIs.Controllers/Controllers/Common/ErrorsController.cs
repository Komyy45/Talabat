using System.Net;
using Linkdev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.APIs.Controllers.Controllers.Common
{
    [ApiController]
    [Route("Errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Errors(int code)
        {
            if(code == (int)HttpStatusCode.NotFound)
            {
               return NotFound(new ApiResponse((int)HttpStatusCode.NotFound, $"The requested Endpoint:{Request.Path} is not found!").ToString());
            }

            return StatusCode(code);
        }
    }
}
