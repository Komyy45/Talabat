using Linkdev.Talabat.Core.Domain.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Linkdev.Talabat.Infrastructure._AttachmentService
{
    internal class AttachmentService : IAttachmentService
	{
		private readonly List<string> validExtentions = new List<string>() { ".jpg", ".jpeg", ".png" };

		private readonly double maxSize = 2_097_152;


		public void Delete(string path)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		public async Task<string?> UploadFile(IFormFile file, string folder)
		{
			var fileExtension = Path.GetExtension(file.FileName);

			if (!validExtentions.Exists(extension => extension.Equals(fileExtension, StringComparison.OrdinalIgnoreCase))) return null;

			if (file.Length > maxSize) return null;

			var folderPath = Path.Combine("D:\\Route Assigments\\ASP.NET Core Web APIs\\Talabat APIs\\Linkdev.Talabat\\Linkdev.Talabat.APIs\\wwwRoot", folder);

			if(!Directory.Exists(folder))
				Directory.CreateDirectory(folderPath);

			var fileName = $"{Guid.NewGuid()}_{file.FileName}";
			
			var filePath = Path.Combine(folderPath, fileName);

			using FileStream stream = new FileStream(filePath,FileMode.Create);

			await file.CopyToAsync(stream);

			return Path.Combine(folder, fileName);
		}
	}
}
