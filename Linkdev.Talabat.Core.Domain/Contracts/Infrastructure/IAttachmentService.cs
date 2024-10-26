using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Linkdev.Talabat.Core.Domain.Contracts.Infrastructure
{
	public interface IAttachmentService
	{
		Task<string?> UploadFile(IFormFile file, string folder);
		
		void Delete(string path);
	}
}
