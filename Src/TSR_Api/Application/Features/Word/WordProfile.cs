using Application.Contracts.TimeLineDTO;
using Application.Contracts.Word.Commands.Create;
using Application.Contracts.Word.Commands.Delete;
using Application.Contracts.Word.Commands.Update;
using Application.Contracts.Word.Responses;

namespace Application.Features.Wordfd
{
    public class WordProfile : Profile
    {
        public WordProfile()
        {
            CreateMap<Words, WordListDto>()
       .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Words, WordDetailsDto>()
                .ForMember(dest => dest.History, opt => opt.MapFrom(src => src.History));
            CreateMap<CreateWordCommand, Words>();
            CreateMap<UpdateWordCommand, Words>();
            CreateMap<DeleteWordCommand, Words>();
            MappingConfiguration.ConfigureVacancyMap<WordTimelineEvent, TimeLineDetailsDto>(this);
        }
    }
}
