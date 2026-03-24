using Application;
using Application.Queries;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Redis
{
    internal class RedisProductCacheService(IDatabase database) : IProductCacheService
    {
        public async Task<ProductQueryResponse?> GetAsync(Guid id)
        {
            var json = await database.StringGetAsync(id.ToString());
            return json.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ProductQueryResponse>(json.ToString());
        }

        public Task CreateAsync(ProductQueryResponse product)
        {
            var json = JsonSerializer.Serialize(product);
            return database.StringSetAsync(product.Id.ToString(), json);
        }

        public Task DeleteAsync(ProductQueryResponse product) =>
            database.StringDeleteAsync(product.Id.ToString(), ValueCondition.Exists);
    }
}
