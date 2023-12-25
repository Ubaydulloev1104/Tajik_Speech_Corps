using MediatR;

namespace TSR_Accoun_Application.Contracts.UserRoles.Commands
{
	public class DeleteUserRoleCommand : IRequest<bool>
	{
		public string Slug { get; set; }
	}
}
