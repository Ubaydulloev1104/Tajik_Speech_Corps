using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Application.Contracts.ApplicationRoles.Queries;
using TSR_Accoun_Application.Contracts.ApplicationRoles.Responses;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.Role.Queries
{
	public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleNameResponse>>
	{
		private readonly RoleManager<ApplicationRole> _roleManager;
		private readonly IMapper _mapper;

		public GetRolesQueryHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
		{
			_roleManager = roleManager;
			_mapper = mapper;
		}

		public async Task<List<RoleNameResponse>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
		{
			var roles = await _roleManager.Roles.ToListAsync();

			var roleResponses = _mapper.Map<List<RoleNameResponse>>(roles);

			return roleResponses;

		}

	}
}
