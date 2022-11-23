using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserApi.Contract.Entities;

namespace OrganizationApi.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.Id);
            builder.Property(c => c.OrganizationId).IsRequired(false);
            builder.Property(c => c.Name);
            builder.Property(c => c.MiddleName);
            builder.Property(c => c.SurName);
            builder.Property(c => c.Email);
            builder.Property(c => c.PhoneNumber);
        }
    }
}
