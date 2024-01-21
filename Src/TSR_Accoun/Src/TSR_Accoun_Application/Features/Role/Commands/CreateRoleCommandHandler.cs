using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TSR_Accoun_Application.Contracts.ApplicationRoles.Commands;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.Role.Commands
{
	public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
	{
		private readonly RoleManager<ApplicationRole> _roleManager;

		public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
		{
			var role = new ApplicationRole
			{
				Id = Guid.NewGuid(),
				Name = request.RoleName,
				NormalizedName = request.RoleName.ToLower(),
				Slug = request.RoleName.ToLower(),
			};

			var result = await _roleManager.CreateAsync(role);
			if (result.Succeeded)
			{
				return role.Id;
			}
			else
			{
				throw new ValidationException();
			}
		}
	}
}
