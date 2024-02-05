using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TSR_Accoun_Application.Common.Exceptions;
using TSR_Accoun_Application.Common.Interfaces.DbContexts;
using TSR_Accoun_Application.Contracts.UserRoles.Commands;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.UserRoles.Commands
{
	public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRolesCommand, string>
	{
		private readonly RoleManager<ApplicationRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IApplicationDbContext _applicationDbContext;

		public CreateUserRoleCommandHandler(RoleManager<ApplicationRole> roleManager,
			UserManager<ApplicationUser> userManager, IApplicationDbContext applicationDbContext)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_applicationDbContext = applicationDbContext;
		}

		public async Task<string> Handle(CreateUserRolesCommand request,
			CancellationToken cancellationToken)
		{
			var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == request.UserName.ToUpper(),
				cancellationToken: cancellationToken);
			_ = user ?? throw new NotFoundException("user is not found");

			var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.NormalizedName == request.RoleName.ToUpper(),
				cancellationToken: cancellationToken);
			if (role == null)
			{
				role = new ApplicationRole
				{
					Id = Guid.NewGuid(),
					Name = request.RoleName,
					NormalizedName = request.RoleName.ToUpper(),
					Slug = request.RoleName.ToLower(),
				};
				var createResult = await _roleManager.CreateAsync(role);
				if (!createResult.Succeeded)
				{
					throw new ValidationException(createResult.Errors.First().Description);
				}
			}

			var newUserRole = new ApplicationUserRole
			{
				UserId = user.Id,
				RoleId = role.Id,
				Slug = $"{user.UserName}-{role.Slug}"
			};
			await _applicationDbContext.UserRoles.AddAsync(newUserRole, cancellationToken);
			await _applicationDbContext.SaveChangesAsync(cancellationToken);


			//add role claim
			var roleClaim = new ApplicationUserClaim
			{
				UserId = user.Id,
				ClaimType = ClaimTypes.Role,
				ClaimValue = role.Name,
				Slug = $"{user.UserName}-role"
			};
			await _applicationDbContext.UserClaims.AddAsync(roleClaim, cancellationToken);
			await _applicationDbContext.SaveChangesAsync(cancellationToken);
			//add role claim

			return newUserRole.Slug;
		}
	}
}
