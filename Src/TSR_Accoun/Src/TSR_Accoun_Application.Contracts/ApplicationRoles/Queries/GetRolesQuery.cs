using MediatR;
using TSR_Accoun_Application.Contracts.ApplicationRoles.Responses;

namespace TSR_Accoun_Application.Contracts.ApplicationRoles.Queries
{
	public class GetRolesQuery : IRequest<List<RoleNameResponse>>
	{
	}
}
