using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProductService.Infrastructure.EfCore
{
    public static class DbInitializer
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ProductContext>();
            context.Database.Migrate();
        }
    }
}
