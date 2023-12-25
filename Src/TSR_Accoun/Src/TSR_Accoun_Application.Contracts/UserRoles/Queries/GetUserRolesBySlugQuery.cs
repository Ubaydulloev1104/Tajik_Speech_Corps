using MediatR;
using TSR_Accoun_Application.Contracts.UserRoles.Response;

namespace TSR_Accoun_Application.Contracts.UserRoles.Queries
{
	public class GetUserRolesBySlugQuery : IRequest<UserRolesResponse>
	{
		public string Slug { get; set; }
	}
}
