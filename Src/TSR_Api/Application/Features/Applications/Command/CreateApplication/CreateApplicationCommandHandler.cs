using Application.Common.SlugGeneratorService;
using Application.Contracts.Applications.Commands.CreateApplication;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Applications.Command.CreateApplication
{
    public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISlugGeneratorService _slugService;

        public CreateApplicationCommandHandler(IApplicationDbContext context, IMapper mapper, IDateTime dateTime,
            ICurrentUserService currentUserService, ISlugGeneratorService slugService)
        {
            _context = context;
            _mapper = mapper;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
            _slugService = slugService;
           
        }

        public async Task<Guid> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            var words = await _context.Words.FindAsync(request.WordId);
            _ = words ?? throw new NotFoundException(nameof(Words), request.WordId);
            var application = _mapper.Map<Domain.Entities.Application>(request);

            application.Slug = GenerateSlug(_currentUserService.GetUserName(), words);

            if (await ApplicationExits(application.Slug))
                throw new ConflictException("Duplicate Apply. You have already submitted your application!");

            application.ApplicantId = _currentUserService.GetUserId() ?? Guid.Empty;
            application.ApplicantUsername = _currentUserService.GetUserName() ?? string.Empty;

            await _context.Applications.AddAsync(application, cancellationToken);
            ApplicationTimelineEvent timelineEvent = new()
            {
                ApplicationId = application.Id,
                EventType = TimelineEventType.Created,
                Time = _dateTime.Now,
                Note = "words created",
                CreateBy = _currentUserService.GetUserId() ?? Guid.Empty
            };

            await _context.ApplicationTimelineEvents.AddAsync(timelineEvent, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return application.Id;
        }

        private async Task<bool> ApplicationExits(string applicationSlug)
        {
            var application = await _context.Applications.FirstOrDefaultAsync(a => a.Slug.Equals(applicationSlug));
            if (application == null)
                return false;
            return true;
        }

        private string GenerateSlug(string username, Words words)
        {
            return _slugService.GenerateSlug($"{username}-{words.Slug}");
        }
    }
}
