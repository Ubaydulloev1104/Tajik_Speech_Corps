using MediatR;
using TSR_Accoun_Application.Contracts.ApplicationRoles.Responses;

namespace TSR_Accoun_Application.Contracts.ApplicationRoles.Queries
{
	public class GetRoleBySlugQuery : IRequest<RoleNameResponse>
	{
		public string Slug { get; set; }
	}
}
