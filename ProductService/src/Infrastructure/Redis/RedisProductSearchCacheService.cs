using Application;
using Application.Queries;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Redis
{
    internal class RedisProductSearchCacheService(IDatabase database) : IProductSerchCacheService
    {
        public async Task<List<ProductQueryResponse>?> GetAsync(string id)
        {
            var json = await database.StringGetAsync(id);
            return json.IsNullOrEmpty ? null : JsonSerializer.Deserialize<List<ProductQueryResponse>>(json.ToString());
        }

        public Task CreateAsync(string id, List<ProductQueryResponse> products)
        {
            var json = JsonSerializer.Serialize(products);
            return database.StringSetAsync(id, json, TimeSpan.FromMinutes(3));
        }
    }
}
