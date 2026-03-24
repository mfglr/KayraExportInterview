using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure.EfCore
{
    public static class DbInitializer
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AuthContext>();
            context.Database.Migrate();
        }
    }
}
