using Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(member => member.Id);

        builder.Property(member => member.FirstName).IsRequired().HasMaxLength(50);

        builder.Property(member => member.LastName).IsRequired().HasMaxLength(50);

        builder.Property(member => member.Email).IsRequired().HasMaxLength(50);

        builder.Property(member => member.Password).IsRequired().HasMaxLength(50);
    }
}
