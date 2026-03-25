using ProductService.Application;
using ProductService.Application.Queries;
using StackExchange.Redis;
using System.Text.Json;

namespace ProductService.Infrastructure.Redis
{
    internal class RedisProductCacheService(IDatabase database) : IProductCacheService
    {
        private readonly static string _productListVersionKey = "product:list:version";
        private Task UpdateProductListVersion() => database.StringIncrementAsync(_productListVersionKey, 1);
        private async Task<long> GetProductListVersion()
        {
            var result = await database.StringGetAsync(_productListVersionKey);
            return result.HasValue ? (long)result : 0;
        }

        public async Task<ProductQueryResponse?> GetAsync(Guid id)
        {
            var json = await database.StringGetAsync(id.ToString());
            return json.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ProductQueryResponse>(json.ToString());
        }
        
        public async Task UpsertAsync(ProductQueryResponse product)
        {
            var json = JsonSerializer.Serialize(product);
            await database.StringSetAsync(product.Id.ToString(), json);
            await UpdateProductListVersion();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            await database.StringDeleteAsync(id.ToString(), ValueCondition.Exists);
            await UpdateProductListVersion();
        }

        public async Task DeleteAsync(IEnumerable<Guid> ids)
        {
            await database.KeyDeleteAsync([.. ids.Select(g => (RedisKey)g.ToString())]);
            await UpdateProductListVersion();
        }


        private static string Id(DateTime? cursor, int pageSize,long version) =>
            cursor == null ? $"list:{pageSize}:{version}" : $"list:{pageSize}:{cursor}:{version}";

        public async Task<List<ProductQueryResponse>?> GetAsync(int pageSize, DateTime? cursor)
        {
            var version = await GetProductListVersion();
            var id = Id(cursor, pageSize,version);
            var json = await database.StringGetAsync(id);
            return json.IsNullOrEmpty ? null : JsonSerializer.Deserialize<List<ProductQueryResponse>>(json.ToString());
        }
        
        public async Task UpsertAsync(int pageSize, DateTime? cursor, List<ProductQueryResponse> products)
        {
            var version = await GetProductListVersion();
            var id = Id(cursor, pageSize,version);
            var json = JsonSerializer.Serialize(products);
            await database.StringSetAsync(id, json);
        }
    }
}
