using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Application.Common.Interfaces.DbContexts;
using TSR_Accoun_Application.Contracts.Claim.Queries;
using TSR_Accoun_Application.Contracts.Claim.Responses;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.Claims.Queries
{
	public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<UserClaimsResponse>>
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IApplicationDbContext _context;

		public GetAllQueryHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context)
		{
			_userManager = userManager;
			_context = context;
		}

		public async Task<List<UserClaimsResponse>> Handle(GetAllQuery request,
			CancellationToken cancellationToken)
		{
			var usersQuery = _userManager.Users;
			if (request.Username != null)
			{
				usersQuery = usersQuery.Where(s => s.NormalizedUserName == request.Username.Trim().ToLower());
			}

			var users = await usersQuery.ToArrayAsync(cancellationToken: cancellationToken);

			IQueryable<ApplicationUserClaim> claimsQuery = _context.UserClaims;
			if (request.ClaimType != null)
			{
				claimsQuery = claimsQuery.Where(s => s.ClaimType!.Contains(request.ClaimType));
			}

			var result = (from applicationUser in users
						  let claims = claimsQuery.Where(s => s.UserId == applicationUser.Id)
						  from claim in claims
						  select new UserClaimsResponse
						  {
							  Username = applicationUser.UserName!,
							  ClaimType = claim.ClaimType!,
							  ClaimValue = claim.ClaimValue!,
							  Slug = claim.Slug
						  }).ToList();

			return result;

		}
	}
}
