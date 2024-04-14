using Application.Contracts.Applications.Commands.CreateApplication;
using Application.Contracts.Applications.Commands.UpdateApplication;
using Application.Contracts.Applications.Responses;
using Application.Contracts.Dtos.Enums;
using Application.Contracts.TimeLineDTO;

namespace Application.Features.Applications
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Domain.Entities.Application, ApplicationListDto>()
            .ForMember(dest => dest.WordResponseAudioDto, opt => opt.MapFrom(src => src.Audios))
            .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.WordValoe, opt => opt.MapFrom(src => src.Words.Value))
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.ApplicantUsername));

            CreateMap<Domain.Entities.Application, ApplicationDetailsDto>()
                .ForMember(dest => dest.WordResponseAudioDto, opt => opt.MapFrom(src => src.Audios))
                .ForMember(dest => dest.History, opt => opt.MapFrom(src => src.History));
            CreateMap<CreateApplicationCommand, Domain.Entities.Application>()
                .ForMember(dest => dest.Audios, opt => opt.MapFrom(src => src.WordResponseAudioDto));

            CreateMap<CreateApplicationWithoutApplicantIdCommand, Domain.Entities.Application>();
            CreateMap<UpdateApplicationCommand, Domain.Entities.Application>();
            MappingConfiguration.ConfigureUserMap<ApplicationTimelineEvent, TimeLineDetailsDto>(this);
            CreateMap<Domain.Entities.Application, ApplicationListStatus>()
                .ForMember(dest => dest.WordValoe, opt => opt.MapFrom(src => src.Words.Value));
            CreateMap<ApplicationStatus, ApplicationStatusDto.ApplicationStatus>();
        }
    }
}
