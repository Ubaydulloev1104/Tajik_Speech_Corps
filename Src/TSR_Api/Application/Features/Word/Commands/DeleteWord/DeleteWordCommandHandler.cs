using Application.Contracts.Word.Commands.Delete;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Word.Commands.DeleteWord
{
    public class DeleteWordCommandHandler : IRequestHandler<DeleteWordCommand, bool>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IApplicationDbContext _dbContext;

        public DeleteWordCommandHandler(IApplicationDbContext dbContext, IDateTime dateTime,
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
        }
        public async Task<bool> Handle(DeleteWordCommand request, CancellationToken cancellationToken)
        {
            var word = await _dbContext.Words.FirstOrDefaultAsync(j => j.Slug == request.Slug, cancellationToken);

            if (word == null)
                throw new NotFoundException(nameof(Words), request.Slug);

            WordTimelineEvent timelineEvent = new WordTimelineEvent
            {
                Words = word,
                WordId = word.Id,
                EventType = TimelineEventType.Deleted,
                Time = _dateTime.Now,
                Note = "word deleted",
                CreateBy = _currentUserService.GetUserId() ?? Guid.Empty
            };

            _dbContext.Words.Remove(word);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
