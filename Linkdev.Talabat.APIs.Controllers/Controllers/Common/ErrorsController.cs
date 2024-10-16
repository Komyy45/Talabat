using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Http;
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
