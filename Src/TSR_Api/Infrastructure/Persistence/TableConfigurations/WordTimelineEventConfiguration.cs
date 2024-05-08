using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.TableConfigurations;

public class WordTimelineEventConfiguration : IEntityTypeConfiguration<WordTimelineEvent>
{
    public void Configure(EntityTypeBuilder<WordTimelineEvent> builder)
    {
        // No need to specify the properties here as they are already defined in the base TimelineEventConfiguration

        // Navigation properties
        builder.HasOne(vte => vte.Words)
            .WithMany(v => v.History)
            .HasForeignKey(vte => vte.WordId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
