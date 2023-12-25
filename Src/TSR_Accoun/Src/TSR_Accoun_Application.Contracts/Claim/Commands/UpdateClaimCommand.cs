using MediatR;

namespace TSR_Accoun_Application.Contracts.Claim.Commands
{
	public class UpdateClaimCommand : IRequest<Unit>
	{
		public string ClaimValue { get; set; } = "";
		public string Slug { get; set; } = "";
	}
}
