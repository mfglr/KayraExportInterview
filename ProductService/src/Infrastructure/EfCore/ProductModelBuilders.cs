using Domain;
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
            builder.OwnsOne(x => x.Price);
            builder.HasIndex(x => new { x.CategoryId, x.Id }).IsDescending(false, true);
        }
    }
}
