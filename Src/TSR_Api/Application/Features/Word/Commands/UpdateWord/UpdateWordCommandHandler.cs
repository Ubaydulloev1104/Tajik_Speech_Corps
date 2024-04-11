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
            var Words = await _dbContext.Words
                .Include(j => j.Category)
                .FirstOrDefaultAsync(j => j.Slug == request.Slug, cancellationToken);
            _ = Words ?? throw new NotFoundException(nameof(Word), request.Slug);

            _mapper.Map(request, Words);

            WordTimelineEvent timelineEvent = new WordTimelineEvent
            {
                Words = Words,
                WordId = Words.Id,
                EventType = TimelineEventType.Updated,
                Time = _dateTime.Now,
                Note = "Word updated",
                CreateBy = _currentUserService.GetUserId() ?? Guid.Empty
            };
            await _dbContext.WordTimelineEvents.AddAsync(timelineEvent, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Words.Slug;
        }
    }
}
