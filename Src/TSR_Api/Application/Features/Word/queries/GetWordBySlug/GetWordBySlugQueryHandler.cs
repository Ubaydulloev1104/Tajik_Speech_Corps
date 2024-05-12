using Application.Contracts.Word.Queries.GetWordBySlug;
using Application.Contracts.Word.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Word.queries.GetWordBySlug
{
    public class GetWordBySlugQueryHandler(
     IApplicationDbContext dbContext,
     IMapper mapper,
     ICurrentUserService currentUser)
     : IRequestHandler<GetWordBySlugQuery, WordDetailsDto>
    {
        public async Task<WordDetailsDto> Handle(GetWordBySlugQuery request,
            CancellationToken cancellationToken)
        {
            Words word = await dbContext.Words.FirstOrDefaultAsync(i => i.Slug == request.Slug, cancellationToken: cancellationToken);
            _ = word ?? throw new NotFoundException(nameof(Words), request.Slug);

            word.History =
                await dbContext.WordTimelineEvents.Where(t => t.WordId == word.Id)
                    .ToListAsync(cancellationToken: cancellationToken);

            var mapped = mapper.Map<WordDetailsDto>(word);
            mapped.IsApplied = await dbContext.Applications.AnyAsync(
                s => s.ApplicantUsername == currentUser.GetUserName() && s.WordId == word.Id,
                cancellationToken: cancellationToken);
            return mapped;
        }
    }
}
