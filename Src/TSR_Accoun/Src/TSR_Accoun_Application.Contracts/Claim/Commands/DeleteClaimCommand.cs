using MediatR;

namespace TSR_Accoun_Application.Contracts.Claim.Commands
{
	public class DeleteClaimCommand : IRequest<Unit>
	{
		public string Slug { get; set; }
	}
}
