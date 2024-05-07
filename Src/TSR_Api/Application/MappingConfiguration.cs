using Application.Contracts.TimeLineDTO;

namespace Application;

public class MappingConfiguration
{
    public static void ConfigureUserMap<TSource, TDestination>(Profile profile)
    {
        if (typeof(TSource) == typeof(ApplicationTimelineEvent) && typeof(TDestination) == typeof(TimeLineDetailsDto))
        {
            profile.CreateMap<ApplicationTimelineEvent, TimeLineDetailsDto>();
        }
    }
    public static void ConfigureVacancyMap<TSource, TDestination>(Profile profile)
    {
        if (typeof(TSource) == typeof(WordTimelineEvent) && typeof(TDestination) == typeof(TimeLineDetailsDto))
        {
            profile.CreateMap<WordTimelineEvent, TimeLineDetailsDto>();

        }
    }
}
