using Application;
using Application.Queries;
using Application.Queries.SearchProduct;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Redis
{
    internal class RedisProductListCacheService(IDatabase database) : IProductListCacheService
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
