using ProductService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EfCore
{
    internal class ProductModelBuilders : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.RowVersion).IsRowVersion();
            builder.OwnsOne(x => x.Title);
            builder.OwnsOne(x => x.Description);
            builder.OwnsOne(
                x => x.Price,
                x => {
                    x.OwnsOne(x => x.Currency);
                    x.Property(x => x.Value).HasPrecision(18, 4);
                }
            );
            builder.HasIndex(x => new { x.CategoryId, x.Id }).IsDescending(false, true);
        }
    }
}
