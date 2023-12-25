using MediatR;

namespace TSR_Accoun_Application.Contracts.ApplicationRoles.Commands
{
	public class CreateRoleCommand : IRequest<Guid>
	{
		public string RoleName { get; set; }
	}
}
