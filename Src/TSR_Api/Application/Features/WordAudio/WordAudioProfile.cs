using Application.Contracts.Dtos;

namespace Application.Features.WordAudio
{
    public class WordAudioProfile : Profile
    {
        public WordAudioProfile() {
        
        CreateMap<WordResponseAudioDto, Audio>();
        }
    }
}
