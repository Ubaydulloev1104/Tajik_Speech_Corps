using Sieve.Services;
namespace Application.Features.Word
{
    public class SieveConfigurationForWord : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Words>(p => p.Value)
               .CanFilter()
               .CanSort();

            mapper.Property<Words>(p => p.Category.Name)
                .CanFilter()
                .HasName(nameof(Words.Category));

            mapper.Property<Words>(p => p.CategoryId)
                .CanFilter();

            mapper.Property<Words>(p => p.Description)
                .CanFilter()
                .CanSort();

            mapper.Property<Words>(p => p.CreateDate)
                .CanFilter()
                .CanSort();

            mapper.Property<Words>(p => p.UpdatedDate)
                .CanFilter()
                .CanSort();
        }
    }
}
