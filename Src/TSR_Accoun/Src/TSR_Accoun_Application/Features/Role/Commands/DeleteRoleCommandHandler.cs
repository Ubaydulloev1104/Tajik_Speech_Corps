using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Application.Common.Exceptions;
using TSR_Accoun_Application.Contracts.ApplicationRoles.Commands;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.Role.Commands
{
	public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
	{
		private readonly RoleManager<ApplicationRole> _roleManager;

		public DeleteRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
		{
			var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Slug == request.Slug);
			_ = role ?? throw new NotFoundException("role not found");
			await _roleManager.DeleteAsync(role);
			return true;

		}
	}
}
