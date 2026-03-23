using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EfCore
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
