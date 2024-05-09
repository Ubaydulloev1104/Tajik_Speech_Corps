using Application.Contracts.Applications.Commands.AddNote;
using Application.Contracts.TimeLineDTO;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Applications.Command.AddNote;

public class AddNoteToApplicationCommandHandler(
    IApplicationDbContext dbContext,
    IMapper mapper,
    IDateTime dateTime,
    ICurrentUserService currentUserService)
    : IRequestHandler<AddNoteToApplicationCommand, TimeLineDetailsDto>
{
    public async Task<TimeLineDetailsDto> Handle(AddNoteToApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = await dbContext.Applications.FirstOrDefaultAsync(a => a.Slug == request.Slug, cancellationToken);
        _ = application ?? throw new NotFoundException(nameof(Application), request.Slug); ;

        ApplicationTimelineEvent timelineEvent = new()
        {
            ApplicationId = application.Id,
            Application = application,
            EventType = TimelineEventType.Note,
            Time = dateTime.Now,
            Note = request.Note,
        };
        await dbContext.ApplicationTimelineEvents.AddAsync(timelineEvent, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<TimeLineDetailsDto>(timelineEvent);
    }
}