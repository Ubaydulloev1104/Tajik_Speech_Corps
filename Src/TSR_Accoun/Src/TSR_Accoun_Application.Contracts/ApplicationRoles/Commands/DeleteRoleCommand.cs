using MediatR;

namespace TSR_Accoun_Application.Contracts.ApplicationRoles.Commands
{
	public class DeleteRoleCommand : IRequest<bool>
	{
		public string Slug { get; set; }
	}
}
