using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.TableConfigurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Domain.Entities.Application>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Application> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
            builder.Property(a => a.AppliedAt).HasColumnType("datetime2");
            builder.Property(a => a.Status).HasColumnType("int");


            builder.HasOne(a => a.Words)
                .WithMany(v => v.Applications)
                .HasForeignKey(a => a.WordId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(a => a.History)
                .WithOne(ate => ate.Application)
                .HasForeignKey(ate => ate.ApplicationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
