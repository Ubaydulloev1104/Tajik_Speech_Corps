using AutoMapper;
using TSR_Accoun_Application.Contracts.ApplicationRoles.Responses;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.Role
{
	public class RoleProfile : Profile
	{
		public RoleProfile()
		{
			CreateMap<ApplicationRole, RoleNameResponse>();

		}
	}
}
