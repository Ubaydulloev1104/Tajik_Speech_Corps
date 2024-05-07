using Application.Contracts.Word.Queries.GetWordBySlug;
using Application.Contracts.Word.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Word.queries.GetWordBySlug
{
    public class GetWordBySlugQueryHandler : IRequestHandler<GetWordBySlugQuery, WordDetailsDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetWordBySlugQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<WordDetailsDto> Handle(GetWordBySlugQuery request, CancellationToken cancellationToken)
        {
            Words words = await _dbContext.Words.Include(i => i.History)
                .FirstOrDefaultAsync(i => i.Slug == request.Slug);
            _ = words ?? throw new NotFoundException(nameof(Words), request.Slug);
            return _mapper.Map<WordDetailsDto>(words);
        }
    }
}
