using MediatR;

namespace TSR_Accoun_Application.Contracts.Claim.Commands
{
#nullable disable
	public class CreateClaimCommand : IRequest<Guid>
	{
		public Guid UserId { get; set; }
		public string ClaimType { get; set; }
		public string ClaimValue { get; set; }
	}
}
