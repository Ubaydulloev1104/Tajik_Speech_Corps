using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Application.Common.Interfaces.DbContexts;
using TSR_Accoun_Application.Contracts.Educations.Query;
using TSR_Accoun_Application.Contracts.Educations.Responsess;

namespace TSR_Accoun_Application.Features.Educations.Queries
{
	public class GetAllEducationsQueryHandler : IRequestHandler<GetAllEducationsQuery, List<UserEducationResponse>>
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly IMapper _mapper;

		public GetAllEducationsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}
		public async Task<List<UserEducationResponse>> Handle(GetAllEducationsQuery request, CancellationToken cancellationToken)
		{
			var educations = await _dbContext.Educations
				.ToListAsync();
			var response = educations.
				Select(e => _mapper.Map<UserEducationResponse>(e)).ToList();
			return response;
		}
	}
}
