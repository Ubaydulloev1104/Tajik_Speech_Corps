using Application.Common.SlugGeneratorService;
using Application.Contracts.WordCategore.Commands.CreateWordCategory;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WordCategories.Command.CreateWordCategory;

public class CreateWordCategoryCommandHandler : IRequestHandler<CreateWordCategoryCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISlugGeneratorService _slugService;

    public CreateWordCategoryCommandHandler(ISlugGeneratorService slugService, IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _slugService = slugService;
    }

    public async Task<string> Handle(CreateWordCategoryCommand request, CancellationToken cancellationToken)
    {
        var wordCategory = _mapper.Map<WordCategory>(request);
        wordCategory.Slug = GenerateSlug(wordCategory);
        var cat = await _context.Categories.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Name == wordCategory.Name && c.IsDeleted);
        if (cat is not null)
        {
            cat.IsDeleted = false;
        }
        else if (!_context.Categories.Any(c => c.Name == request.Name))
        {
            await _context.Categories.AddAsync(wordCategory, cancellationToken);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return wordCategory.Slug;
    }
    private string GenerateSlug(WordCategory category)
    {
        var slug = _slugService.GenerateSlug($"{category.Name}");
        var count = _context.Categories.Count(c => c.Slug == slug);
        if (count == 0)
            return slug;

        return $"{slug}-{DateTime.Now.ToString("yyyyMMddHHmmss")}";
    }

}