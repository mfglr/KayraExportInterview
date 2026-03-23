using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EfCore
{
    public static class DpInitializer
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProductContext>();
            context.Database.Migrate();
        }
    }
}
