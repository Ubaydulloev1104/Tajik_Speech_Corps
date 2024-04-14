using Sieve.Services;

namespace Application.Features.Applications
{
    public class SieveConfigurationForApplication : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Domain.Entities.Application>(p => p.ApplicantId)
                .CanFilter();

            mapper.Property<Domain.Entities.Application>(p => p.Words.Value)
                .CanFilter()
                .HasName(nameof(Domain.Entities.Application.Words));

            mapper.Property<Domain.Entities.Application>(p => p.WordId)
                .CanFilter();


            mapper.Property<Domain.Entities.Application>(p => p.Status)
                .CanFilter()
                .CanSort();
        }
    }
}
