using Linkdev.Talabat.APIs.Controllers.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("NotFound")] // GET : "/api/Buggy/NotFound"
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(new { StatusCode = 404, Message = "NotFound" });
        }

        [HttpGet("ServerError")] // GET : "/api/Buggy/ServerError"
        public IActionResult GetServerError()
        {
            throw new InvalidCastException();
        }

        [HttpGet("BadRequest")] // GET : "/api/Buggy/BadRequest"
        public IActionResult GetBadRequest()
        {
            return BadRequest(new { StatusCode = 400, Message = "BadRequest" });
        }

        [HttpGet("ValidationError/{id}")] // GET : "/api/Buggy/ValidationError/{id}"
        public IActionResult GetValidationError(int id)
        {
            return Ok();
        }

        [HttpGet("unauthorized")] // GET : "/api/Buggy/unatuhorized"
        public IActionResult GetUnauthorizedError()
        {
            return Unauthorized(new { StatusCode = 401, Message = "Unauthorized" });
        }

        [Authorize]
        [HttpGet("unauthorizedRequest")] // GET : "/api/Buggy/unauthorizedRequest"
        public IActionResult GetUnauthorizedRequest()
        {
            return Ok();
        }

        [HttpGet("forbidden")] // GET : "/api/Buggy/forbidden"
        public IActionResult GetForbiddenRequest()
        {
            return Forbid();
        }

    }
}
