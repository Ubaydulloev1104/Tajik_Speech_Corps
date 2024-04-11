using Application.Common.Sieve;
using Application.Contracts.Common;
using Application.Contracts.Word.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Word.queries.GetWordWithPagination
{
    public class GetWordPagedQueryHandler : IRequestHandler<PagedListQuery<WordListDto>, PagedList<WordListDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IApplicationSieveProcessor _sieveProcessor;

        public GetWordPagedQueryHandler(IApplicationDbContext dbContext, IApplicationSieveProcessor sieveProcessor,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _sieveProcessor = sieveProcessor;
            _mapper = mapper;
        }

        public async Task<PagedList<WordListDto>> Handle(PagedListQuery<WordListDto> request,
            CancellationToken cancellationToken)
        {
            PagedList<WordListDto> result = _sieveProcessor.ApplyAdnGetPagedList(request,
                _dbContext.Words
                .Include(j => j.Category)
                .AsNoTracking(),
                _mapper.Map<WordListDto>);
            return await Task.FromResult(result);
        }
    }
}
