using System.Text.Json;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Infrastructure.Caching;
using StackExchange.Redis;

namespace Linkdev.Talabat.Infrastructure.Caching
{
	internal class CacheService(IConnectionMultiplexer connectionMultiplexer) : ICacheService
	{
		private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

		public async Task CacheResponseAsync(string key, object response, TimeSpan timeToLive)
		{
			if (key is null || response is null) return;

			var options = new JsonSerializerOptions()
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			};

			var responseAsJson = JsonSerializer.Serialize(response, options);

			await _database.StringSetAsync(key, responseAsJson, timeToLive);
		}

		public async Task<string?> GetCachedReponseAsync(string key)
		{
			if (key is null) return null!;

			return await _database.StringGetAsync(key);
		}
	}
}
