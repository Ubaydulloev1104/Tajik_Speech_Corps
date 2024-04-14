using Application.Contracts.Applications.Commands.CreateApplication;

namespace Application.Features.Applications.Command.CreateApplication
{
    public class CreateApplicationWithoutApplicantIdCommandHandler : IRequestHandler<CreateApplicationWithoutApplicantIdCommand,
        Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IMapper _mapper;

        public CreateApplicationWithoutApplicantIdCommandHandler(IApplicationDbContext context, IMapper mapper,
            IDateTime dateTime, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
        }
        public async Task<Guid> Handle(CreateApplicationWithoutApplicantIdCommand request,
        CancellationToken cancellationToken)
        {
            Words words = await _context.Words.FindAsync(request.WordId);
            _ = words ?? throw new NotFoundException(nameof(Words), request.WordId);

            var application = _mapper.Map<Domain.Entities.Application>(request);

            // temp: Gets username from Claims
            string username = "temp";
            application.Slug = GenerateSlug(username, words);

            await _context.Applications.AddAsync(application, cancellationToken);

            ApplicationTimelineEvent timelineEvent = new ApplicationTimelineEvent
            {
                ApplicationId = application.Id,
                EventType = TimelineEventType.Created,
                Time = _dateTime.Now,
                Note = "words created",
                CreateBy = _currentUserService.GetUserId().Value
            };

            await _context.ApplicationTimelineEvents.AddAsync(timelineEvent, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return application.Id;
        }

        private string GenerateSlug(string username, Words words)
        {
            //Here instead of the applicant.Firstname should be used applicnat.Username,
            //beacuse the applicant model should be redesigned, i used Firstname temparoraly.
            return $"{username.ToLower().Trim()}-{words.Value.ToLower().Trim()}";
        }
    }
}

