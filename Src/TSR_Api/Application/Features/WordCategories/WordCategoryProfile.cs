using Application.Contracts.WordCategore.Commands.CreateWordCategory;
using Application.Contracts.WordCategore.Commands.UpdateWordCategory;
using Application.Contracts.WordCategore.Responses;

namespace Application.Features.WordCategories;

public class WordCategoryProfile : Profile
{
    public WordCategoryProfile()
    {
        CreateMap<CreateWordCategoryCommand, WordCategory>();
        CreateMap<UpdateWordCategoryCommand, WordCategory>();
        CreateMap<WordCategory, CategoryResponse>();
    }
}
