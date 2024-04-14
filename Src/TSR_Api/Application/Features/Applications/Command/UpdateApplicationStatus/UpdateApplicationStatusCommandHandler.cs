using Application.Contracts.Applications.Commands.UpdateApplicationStatus;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Applications.Command.UpdateApplicationStatus
{
    public class UpdateApplicationStatusCommandHandler : IRequestHandler<UpdateApplicationStatuss, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;

        public UpdateApplicationStatusCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IDateTime dateTime, ICurrentUserService currentUserService)
        {
            _context = dbContext;
            _mapper = mapper;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
        }
        public async Task<bool> Handle(UpdateApplicationStatuss request, CancellationToken cancellationToken)
        {
            var application = await _context.Applications.FirstOrDefaultAsync(t => t.Slug == request.Slug, cancellationToken);
            _ = application ?? throw new NotFoundException(nameof(Application), request.Slug); ;

            application.Status = (ApplicationStatus)request.StatusId;

            var timelineEvent = new ApplicationTimelineEvent
            {
                ApplicationId = application.Id,
                Application = application,
                EventType = TimelineEventType.Updated,
                Time = _dateTime.Now,
                Note = "Application updated",
                CreateBy = _currentUserService.GetUserId() ?? Guid.Empty
            };
            await _context.ApplicationTimelineEvents.AddAsync(timelineEvent);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

}
