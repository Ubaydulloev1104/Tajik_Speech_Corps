using MediatR;
using TSR_Accoun_Application.Contracts.Claim.Responses;

namespace TSR_Accoun_Application.Contracts.Claim.Queries
{
	public class GetBySlugQuery : IRequest<UserClaimsResponse>
	{
		public string Slug { get; set; } = "";
	}
}
