using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Application.Common.Exceptions;
using TSR_Accoun_Application.Common.Interfaces.DbContexts;
using TSR_Accoun_Application.Common.Interfaces.Services;
using TSR_Accoun_Application.Contracts.Educations.Query;
using TSR_Accoun_Application.Contracts.Educations.Responsess;

namespace TSR_Accoun_Application.Features.Educations.Queries
{
	public class GetAllEducationsByUserQueryHandler : IRequestHandler<GetEducationsByUserQuery, List<UserEducationResponse>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IUserHttpContextAccessor _userHttpContextAccessor;
		private readonly IMapper _mapper;

		public GetAllEducationsByUserQueryHandler(IApplicationDbContext context,
			IUserHttpContextAccessor userHttpContextAccessor, IMapper mapper)
		{
			_context = context;
			_userHttpContextAccessor = userHttpContextAccessor;
			_mapper = mapper;
		}

		public async Task<List<UserEducationResponse>> Handle(GetEducationsByUserQuery request,
			CancellationToken cancellationToken)
		{
			var roles = _userHttpContextAccessor.GetUserRoles();
			var userName = _userHttpContextAccessor.GetUserName();
			if (request.UserName != null && roles.Any(role => role == "Applicant") && userName != request.UserName)
				throw new ForbiddenAccessException("Access is denied");

			if (request.UserName != null)
				userName = request.UserName;

			var user = await _context.Users
				.Include(u => u.Educations)
				.FirstOrDefaultAsync(u => u.UserName == userName);
			_ = user ?? throw new NotFoundException("user is not found");

			var userEducationResponses = user.Educations
				.Select(e => _mapper.Map<UserEducationResponse>(e)).ToList();

			return userEducationResponses;
		}
	}

}
