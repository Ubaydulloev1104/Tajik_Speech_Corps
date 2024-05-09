using Slugify;

namespace Application.Common.SlugGeneratorService;

public class SlugGeneratorService : ISlugGeneratorService
{
    static readonly SlugHelper slugHelper = new();

    public string GenerateSlug(string inputText)
    {
        return slugHelper.GenerateSlug(inputText);
    }
}
