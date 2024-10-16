using Linkdev.Talabat.APIs.Controllers.Controllers.Base;
using Linkdev.Talabat.APIs.Controllers.Errors;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("NotFound")] // GET : "/api/Buggy/NotFound"
        public IActionResult GetNotFoundRequest()
        {
            // throw new NotFoundException();
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("ServerError")] // GET : "/api/Buggy/ServerError"
        public IActionResult GetServerError()
        {
            throw new InvalidCastException();
        }

        [HttpGet("BadRequest")] // GET : "/api/Buggy/BadRequest"
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("ValidationError/{id}")] // GET : "/api/Buggy/ValidationError/{id}"
        public IActionResult GetValidationError(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiValidationErrorResponse(400)
                {
                    Errors = ModelState.Where(state => state.Value?.Errors.Count > 0)
                                        .Select(state => new { state.Key, Errors = state.Value.Errors.Select(error => error.ErrorMessage) })
                                        .ToDictionary(error => error.Key, error => error.Errors)
                });
            }

            return Ok();
        }

        [HttpGet("unauthorized")] // GET : "/api/Buggy/unatuhorized"
        public IActionResult GetUnauthorizedError()
        {
            return Unauthorized(new ApiResponse(401));
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
