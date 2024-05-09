using Application.Contracts.Word;
using Application.Contracts.Word.Queries.GetWordCategories;
using Application.Contracts.Word.Responses;
using Application.Contracts.WordCategore.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WordCategories.Queries;

public class GetWordCategoriesQueryHandler : IRequestHandler<GetWordCategoriesQuery, List<WordCategoriesResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWordCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WordCategoriesResponse>> Handle(GetWordCategoriesQuery request, CancellationToken cancellationToken)
    {

        IEnumerable<Words> wordsQuery = _context.Words;
        if (request.CheckDate)
        {
            DateTime now = DateTime.Now;
            wordsQuery = _context.Words.Where(j => j.CreateDate.AddDays(-1) <= now && j.UpdatedDate >= now);

        }
        var sortredword = (from j in wordsQuery
                           group j by j.CategoryId).ToList();

        var words = wordsQuery.ToList();

        var WordsWithCategory = new List<WordCategoriesResponse>();
        var categories = await _context.Categories
           .Where(c => c.Slug != CommonWordSlugs.NoWordSlug)
           .ToListAsync(cancellationToken: cancellationToken);

        foreach (var word in sortredword)
        {
            var category = categories.Where(c => c.Id == word.Key).FirstOrDefault();

            WordsWithCategory.Add(new WordCategoriesResponse
            {
                CategoryId = word.Key,
                Category = _mapper.Map<CategoryResponse>(category),
                WordCount = word.Count()
            });
        }

        return WordsWithCategory;
    }
}
