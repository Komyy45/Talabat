using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Infrastructure.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Linkdev.Talabat.APIs.Controllers.Filters
{
	public class CachedAttribute(int timeToLiveInSeconds) : Attribute, IAsyncActionFilter
	{

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

			var key = GeneratedRequestKey(context.HttpContext);
			var cachedReponse = await cacheService.GetCachedReponseAsync(key);
			if (cachedReponse is not null)
			{
				var contentResult = new ContentResult()
				{
					Content = cachedReponse,
					ContentType = "application/json",
					StatusCode = 200,
				};

				context.Result = contentResult;
				return;
			}
			
			var actionExecutionResult = await next();

			var response = actionExecutionResult.Result;

			if(response is OkObjectResult objectResult)
			{
				await cacheService.CacheResponseAsync(key, objectResult.Value!, TimeSpan.FromSeconds(timeToLiveInSeconds));

			}
		}

		private string GeneratedRequestKey(HttpContext httpContext)
		{
			StringBuilder requestKey = new StringBuilder(httpContext.Request.Path);

			foreach(var (key, value) in httpContext.Request.Query.OrderBy(x => x))
			{
				requestKey.Append($"&{key}={value}");
			}

			return requestKey.ToString();
		}
	}
}
