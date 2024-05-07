using Application.Common.SlugGeneratorService;
using Application.Contracts.Word.Commands.Update;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Word.Commands.UpdateWord
{
    public class UpdateWordCommandHandler : IRequestHandler<UpdateWordCommand, string>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISlugGeneratorService _slugService;

        public UpdateWordCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IDateTime dateTime,
            ICurrentUserService currentUserService, ISlugGeneratorService slugService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
            _slugService = slugService;
        }
        public async Task<string> Handle(UpdateWordCommand request, CancellationToken cancellationToken)
        {
            var words = await _dbContext.Words
                .Include(j => j.Category)
                .FirstOrDefaultAsync(j => j.Slug == request.Slug, cancellationToken);
            _ = words ?? throw new NotFoundException(nameof(Words), request.Slug);

            _mapper.Map(request, words);

            WordTimelineEvent timelineEvent = new WordTimelineEvent
            {
                Words = words,
                WordId = words.Id,
                EventType = TimelineEventType.Updated,
                Time = _dateTime.Now,
                Note = "Word updated",
                CreateBy = _currentUserService.GetUserId() ?? Guid.Empty
            };
            await _dbContext.WordTimelineEvents.AddAsync(timelineEvent, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return words.Slug;
        }
    }
}
