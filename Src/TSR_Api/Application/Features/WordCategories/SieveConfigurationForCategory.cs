using Sieve.Services;

namespace Application.Features.WordCategories;

public class SieveConfigurationForCategory : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<WordCategory>(p => p.Name)
            .CanFilter()
            .CanSort();
    }
}
