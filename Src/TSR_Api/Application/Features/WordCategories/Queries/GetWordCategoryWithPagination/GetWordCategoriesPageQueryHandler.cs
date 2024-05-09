using Application.Common.Sieve;
using Application.Contracts.Common;
using Application.Contracts.WordCategore.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WordCategories.Queries.GetWordCategoryWithPagination;
public class GetVacancyCategoriesPageQueryHandler : IRequestHandler<PagedListQuery<CategoryResponse>,
        PagedList<CategoryResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IApplicationSieveProcessor _sieveProcessor;

    public GetVacancyCategoriesPageQueryHandler(IApplicationDbContext dbContext,
        IApplicationSieveProcessor sieveProcessor, IMapper mapper)
    {
        _dbContext = dbContext;
        _sieveProcessor = sieveProcessor;
        _mapper = mapper;
    }

    public async Task<PagedList<CategoryResponse>> Handle(PagedListQuery<CategoryResponse> request,
        CancellationToken cancellationToken)
    {
        if (_dbContext.Categories != null)
        {
            PagedList<CategoryResponse> result = _sieveProcessor.ApplyAdnGetPagedList(request,
                _dbContext.Categories.AsNoTracking(), _mapper.Map<CategoryResponse>);
            return await Task.FromResult(result);
        }
        else
        {
            Console.WriteLine("Ошибка: таблица Categories недоступна.");
            return new PagedList<CategoryResponse>();
        }

    }

}

