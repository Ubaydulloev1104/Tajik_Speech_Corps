using MediatR;
using TSR_Accoun_Application.Contracts.UserRoles.Response;

namespace TSR_Accoun_Application.Contracts.UserRoles.Queries
{
	public class GetUserRolesQuery : IRequest<List<UserRolesResponse>>
	{
		public string Role { get; set; }
		public string UserName { get; set; }
	}
}
