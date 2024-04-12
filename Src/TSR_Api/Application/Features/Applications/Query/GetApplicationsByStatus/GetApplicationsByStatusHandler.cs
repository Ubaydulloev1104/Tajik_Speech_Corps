using Application.Contracts.Applications.Queries.GetApplicationsByStatus;
using Application.Contracts.Applications.Responses;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Applications.Query.GetApplicationsByStatus
{
    public class GetApplicationsByStatusHandler : IRequestHandler<GetApplicationsByStatusQuery, List<ApplicationListStatus>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetApplicationsByStatusHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ApplicationListStatus>> Handle(GetApplicationsByStatusQuery request,
            CancellationToken cancellationToken)
        {
            List<ApplicationListStatus> applications = await _dbContext.Applications
                .Where(a => a.Status == _mapper.Map<Domain.Enums.ApplicationStatus>(request.Status))
                .ProjectTo<ApplicationListStatus>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return applications;
        }
    }
}
