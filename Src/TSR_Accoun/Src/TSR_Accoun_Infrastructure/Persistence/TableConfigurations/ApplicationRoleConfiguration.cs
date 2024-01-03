using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Infrastructure.Persistence.TableConfigurations
{
	public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
	{
		public void Configure(EntityTypeBuilder<ApplicationRole> builder)
		{
			builder.Property(r => r.Slug).IsRequired();
			builder.HasIndex(r => r.Slug).IsUnique();

		}
	}
}
