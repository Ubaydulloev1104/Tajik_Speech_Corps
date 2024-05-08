using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.TableConfigurations;

public class WordConfiguration : IEntityTypeConfiguration<Words>
{
    public void Configure(EntityTypeBuilder<Words> builder)
    {
        builder.HasQueryFilter(e => !e.IsDeleted);
        // Common properties
        builder.Property(v => v.Value).HasColumnType("nvarchar(256)").IsRequired();
        builder.Property(v => v.Description).HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(v => v.CreateDate).HasColumnType("datetime2");
        builder.Property(v => v.UpdatedDate).HasColumnType("datetime2");

        // Navigation properties
        builder.HasOne(v => v.Category)
            .WithMany(vc => vc.Words)
            .HasForeignKey(v => v.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(v => v.Applications)
            .WithOne(a => a.Words)
            .HasForeignKey(a => a.WordId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(v => v.History)
            .WithOne(vte => vte.Words)
            .HasForeignKey(vte => vte.WordId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(v => v.Slug).IsUnique();
    }
}
