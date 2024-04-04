using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.TableConfigurations
{
    public class AudioConfiguration : IEntityTypeConfiguration<Audio>
    {
        public void Configure(EntityTypeBuilder<Audio> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
           
            builder.Property(v => v.FileName).HasColumnType("nvarchar(256)").IsRequired();
            builder.Property(t => t.Word).HasColumnType("nvarchar(256)");
            
        }
    }
}
