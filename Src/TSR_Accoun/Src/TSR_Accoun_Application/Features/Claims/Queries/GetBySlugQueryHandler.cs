using MediatR;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Application.Common.Exceptions;
using TSR_Accoun_Application.Common.Interfaces.DbContexts;
using TSR_Accoun_Application.Contracts.Claim.Queries;
using TSR_Accoun_Application.Contracts.Claim.Responses;

namespace TSR_Accoun_Application.Features.Claims.Queries
{
	public class GetBySlugQueryHandler : IRequestHandler<GetBySlugQuery, UserClaimsResponse>
	{
		private readonly IApplicationDbContext _context;

		public GetBySlugQueryHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<UserClaimsResponse> Handle(GetBySlugQuery request, CancellationToken cancellationToken)
		{
			var claim = await _context.UserClaims.SingleOrDefaultAsync(s => s.Slug == request.Slug.Trim().ToLower(),
				cancellationToken);
			_ = claim ?? throw new NotFoundException("claim not found");
			return new UserClaimsResponse
			{
				Username = claim.Slug.Split('-').First(),
				ClaimType = claim.ClaimType!,
				ClaimValue = claim.ClaimValue!,
				Slug = claim.Slug
			};
		}
	}
}
