using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.TableConfigurations;

public class WordCategoryConfiguration : IEntityTypeConfiguration<WordCategory>
{
    public void Configure(EntityTypeBuilder<WordCategory> builder)
    {
        builder.HasQueryFilter(e => !e.IsDeleted);
        builder.Property(vc => vc.Name).HasColumnType("nvarchar(128)").IsRequired();

        builder.HasMany(vc => vc.Words)
            .WithOne(v => v.Category)
            .HasForeignKey(v => v.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(v => v.Slug).IsUnique();
    }
}
