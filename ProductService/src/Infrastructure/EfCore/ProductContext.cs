using Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.EfCore
{
    internal class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
