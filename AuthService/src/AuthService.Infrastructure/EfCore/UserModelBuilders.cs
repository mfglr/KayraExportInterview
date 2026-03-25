using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.EfCore
{
    internal class UserModelBuilders : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsMany(x => x.RefreshTokens,refreshToken => refreshToken.Ignore(x => x.Token));
        }
    }
}
