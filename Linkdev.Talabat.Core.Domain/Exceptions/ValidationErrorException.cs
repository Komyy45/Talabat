using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Talabat.Core.Application.Exceptions
{
    public class ValidationErrorException : BadRequestException
    {
        public required IEnumerable<string> Errors { get; set; }

        public ValidationErrorException(string? message) : base(message)
        {
        }
    }
}
