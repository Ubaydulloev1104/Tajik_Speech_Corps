using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Application.Common.Exceptions;
using TSR_Accoun_Application.Contracts.ApplicationRoles.Commands;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.Role.Commands
{
	public class UpdateRolCommandHandler : IRequestHandler<UpdateRoleCommand, Guid>
	{
		private readonly RoleManager<ApplicationRole> _roleManager;
		public UpdateRolCommandHandler(RoleManager<ApplicationRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task<Guid> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
		{
			var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Slug == request.Slug);
			_ = role ?? throw new NotFoundException("Role not found");
			role.Name = request.NewRoleName;
			var result = await _roleManager.UpdateAsync(role);
			return result.Succeeded
					 ? role.Id
					 : throw new ValidationException("Failed to update role.");
		}
	}

}
