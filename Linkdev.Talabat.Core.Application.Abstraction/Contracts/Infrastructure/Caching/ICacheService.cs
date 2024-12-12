using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Talabat.Core.Application.Abstraction.Contracts.Infrastructure.Caching
{
	public interface ICacheService
	{
		Task CacheResponseAsync(string key, object response, TimeSpan timeToLive);

		Task<string?> GetCachedReponseAsync(string key);
	}
}
