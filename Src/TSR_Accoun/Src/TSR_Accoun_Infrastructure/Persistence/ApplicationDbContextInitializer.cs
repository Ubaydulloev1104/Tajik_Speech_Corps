using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TSR_Accoun_Application.Common.Interfaces.DbContexts;
using TSR_Accoun_Domain.Entities;
using TSR_Accoun_Infrastructure.Identity;

namespace TSR_Accoun_Infrastructure.Persistence
{
	public class ApplicationDbContextInitializer
	{
		private readonly RoleManager<ApplicationRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IApplicationDbContext _context;
		private ApplicationRole _superAdminRole = null!;
		private ApplicationRole _applicationRole = null!;

		public ApplicationDbContextInitializer(RoleManager<ApplicationRole> roleManager,
			UserManager<ApplicationUser> userManager, IApplicationDbContext context)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_context = context;
		}

		public async Task SeedAsync()
		{
			await CreateRolesAsync();

			await CreateApplicationAdmin("TSR", "tsr1234$#AU");
		}

		private async Task CreateApplicationAdmin(string applicationName, string adminPassword)
		{
			//create user
			var tsrAdminUser =
				await _userManager.Users.SingleOrDefaultAsync(u =>
					u.NormalizedUserName == $"{applicationName}ADMIN".ToUpper());

			if (tsrAdminUser == null)
			{
				tsrAdminUser = new ApplicationUser
				{
					Id = Guid.NewGuid(),
					UserName = $"{applicationName}Admin",
					NormalizedUserName = $"{applicationName}ADMIN".ToUpper(),
					Email = $"{applicationName.ToLower()}ubaydulloev1104@gmail.com",
				};
				var createMraJobsAdminResult = await _userManager.CreateAsync(tsrAdminUser, adminPassword);
				ThrowExceptionFromIdentityResult(createMraJobsAdminResult);
			}
			//create user

			//create userRole
			var userRole = new ApplicationUserRole
			{
				UserId = tsrAdminUser.Id,
				RoleId = _applicationRole.Id,
				Slug = $"{tsrAdminUser.UserName}-role"
			};

			if (!await _context.UserRoles.AnyAsync(s => s.RoleId == userRole.RoleId && s.UserId == userRole.UserId))
			{
				await _context.UserRoles.AddAsync(userRole);
				await _context.SaveChangesAsync();
			}
			//create userRole

			//create role claim
			if (!await _context.UserClaims.AnyAsync(s =>
					s.UserId == tsrAdminUser.Id &&
					s.ClaimType == ClaimTypes.Role))
			{
				var userRoleClaim = new ApplicationUserClaim
				{
					UserId = tsrAdminUser.Id,
					ClaimType = ClaimTypes.Role,
					ClaimValue = ApplicationClaimValues.Administrator,
					Slug = $"{tsrAdminUser.UserName}-role"
				};
				await _context.UserClaims.AddAsync(userRoleClaim);
			}
			//create role claim

			//create email claim
			if (!await _context.UserClaims.AnyAsync(s =>
					s.UserId == tsrAdminUser.Id &&
					s.ClaimType == ClaimTypes.Role))
			{
				var userRoleClaim = new ApplicationUserClaim
				{
					UserId = tsrAdminUser.Id,
					ClaimType = ClaimTypes.Email,
					ClaimValue = tsrAdminUser.Email,
					Slug = $"{tsrAdminUser.UserName}-email"
				};
				await _context.UserClaims.AddAsync(userRoleClaim);
			}
			//create email claim


			//create application claim
			if (!await _context.UserClaims.AnyAsync(s =>
					s.UserId == tsrAdminUser.Id &&
					s.ClaimType == ClaimTypes.Actor))
			{
				var userApplicationClaim = new ApplicationUserClaim
				{
					UserId = tsrAdminUser.Id,
					ClaimType = ClaimTypes.Role,
					ClaimValue = applicationName,
					Slug = $"{tsrAdminUser.UserName}-application"
				};
				await _context.UserClaims.AddAsync(userApplicationClaim);
			}

			//create application claim

			//create username claim
			if (!await _context.UserClaims.AnyAsync(s =>
					s.UserId == tsrAdminUser.Id &&
					s.ClaimType == ClaimTypes.Name))
			{
				var userApplicationClaim = new ApplicationUserClaim
				{
					UserId = tsrAdminUser.Id,
					ClaimType = ClaimTypes.Name,
					ClaimValue = tsrAdminUser.UserName,
					Slug = $"{tsrAdminUser.UserName}-username"
				};
				await _context.UserClaims.AddAsync(userApplicationClaim);
			}
			//create username claim

			await _context.SaveChangesAsync();
		}
		private async Task CreateRolesAsync()
		{
			_context.Roles.RemoveRange(await _context.Roles.ToListAsync());
			await _context.SaveChangesAsync();

			//superAdmin
			var roleSuper = new ApplicationRole
			{
				Id = Guid.NewGuid(),
				Name = ApplicationClaimValues.SuperAdministrator,
				NormalizedName = ApplicationClaimValues.SuperAdministrator.ToUpper(),
				Slug = ApplicationClaimValues.SuperAdministrator
			};
			var createRoleResult = await _roleManager.CreateAsync(roleSuper);
			ThrowExceptionFromIdentityResult(createRoleResult);
			_superAdminRole = roleSuper;
			//superAdmin

			//applicationAdmin
			var role = new ApplicationRole
			{
				Id = Guid.NewGuid(),
				Name = ApplicationClaimValues.Administrator,
				NormalizedName = ApplicationClaimValues.Administrator.ToUpper(),
				Slug = ApplicationClaimValues.Administrator
			};
			createRoleResult = await _roleManager.CreateAsync(role);
			ThrowExceptionFromIdentityResult(createRoleResult);
			_applicationRole = role;
			//applicationAdmin

			//reviewer
			var roleString = "Reviewer";
			role = new ApplicationRole
			{
				Id = Guid.NewGuid(),
				Name = roleString,
				NormalizedName = roleString.ToUpper(),
				Slug = roleString
			};

			createRoleResult = await _roleManager.CreateAsync(role);
			ThrowExceptionFromIdentityResult(createRoleResult);
			//reviewer

			//applicant
			roleString = "Applicant";
			role = new ApplicationRole
			{
				Id = Guid.NewGuid(),
				Name = roleString,
				NormalizedName = roleString.ToUpper(),
				Slug = roleString
			};

			createRoleResult = await _roleManager.CreateAsync(role);
			ThrowExceptionFromIdentityResult(createRoleResult);
			//applicant


		}

		private static void ThrowExceptionFromIdentityResult(IdentityResult result, [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
		{
			if (!result.Succeeded)
			{
				throw new Exception($"{result.Errors.First().Description}\n exception was thrown at line {sourceLineNumber}");
			}
		}
	}
}
