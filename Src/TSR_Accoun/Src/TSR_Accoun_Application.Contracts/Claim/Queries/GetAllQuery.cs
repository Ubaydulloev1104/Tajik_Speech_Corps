using MediatR;
using TSR_Accoun_Application.Contracts.Claim.Responses;

namespace TSR_Accoun_Application.Contracts.Claim.Queries
{
	public class GetAllQuery : IRequest<List<UserClaimsResponse>>
	{
		public string Username { get; set; } = null;
		public string ClaimType { get; set; } = null;
	}
}
