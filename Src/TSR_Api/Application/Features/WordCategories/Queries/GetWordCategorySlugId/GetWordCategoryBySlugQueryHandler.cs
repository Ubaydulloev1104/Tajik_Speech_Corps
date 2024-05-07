using Application.Contracts.WordCategore.Queries.GetWordCategoryWithPagination;
using Application.Contracts.WordCategore.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WordCategories.Queries.GetWordCategorySlugId;

public class GetWordCategoryBySlugQueryHandler : IRequestHandler<GetWordCategoryBySlugQuery, CategoryResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetWordCategoryBySlugQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<CategoryResponse> Handle(GetWordCategoryBySlugQuery request, CancellationToken cancellationToken)
    {
        var vacancyCategory = await _dbContext.Categories.FirstOrDefaultAsync(v => v.Slug == request.Slug, cancellationToken);
        _ = vacancyCategory ?? throw new NotFoundException(nameof(WordCategory), request.Slug);
        return _mapper.Map<CategoryResponse>(vacancyCategory);
    }
}
