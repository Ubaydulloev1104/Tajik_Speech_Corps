using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Application.Contracts.ApplicationRoles.Queries;
using TSR_Accoun_Application.Contracts.ApplicationRoles.Responses;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.Role.Queries
{
	public class GetRoleGetBySlugQueryHandler : IRequestHandler<GetRoleBySlugQuery, RoleNameResponse>
	{
		private readonly RoleManager<ApplicationRole> _roleManager;
		private readonly IMapper _mapper;

		public GetRoleGetBySlugQueryHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
		{
			_roleManager = roleManager;
			_mapper = mapper;
		}
		public async Task<RoleNameResponse> Handle(GetRoleBySlugQuery request, CancellationToken cancellationToken)
		{
			var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Slug == request.Slug);
			var roleResponses = _mapper.Map<RoleNameResponse>(role);
			return roleResponses;
		}
	}
}
