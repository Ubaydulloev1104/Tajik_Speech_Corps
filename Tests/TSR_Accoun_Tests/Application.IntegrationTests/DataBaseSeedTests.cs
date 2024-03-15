using FluentAssertions;
using System.Security.Claims;
using TSR_Accoun_Domain.Entities;
using TSR_Accoun_Infrastructure.Identity;

namespace Application.IntegrationTests
{

	public class DataBaseSeedTests : BaseTest
	{
		#region SuperAdmin

		private async Task<ApplicationUser> GetSuperAdmin() =>
		await GetEntity<ApplicationUser>(s => s.UserName == "SuperAdmin");

		[Test]
		public async Task SuperAdminRole()
		{
			var superAdminRole = await GetEntity<ApplicationRole>(s =>
				s.NormalizedName == ApplicationClaimValues.SuperAdministrator.ToUpper());
			superAdminRole.Should().NotBeNull();
		}

		[Test]
		public async Task SuperAdminUser()
		{
			(await GetSuperAdmin()).Should().NotBeNull();
		}

		[Test]
		public async Task SuperAdminClaims()
		{
			var superAdmin = await GetSuperAdmin();

			var roleClaim = await GetEntity<ApplicationUserClaim>(s =>
				s.UserId == superAdmin.Id &&
				s.ClaimType == ClaimTypes.Role);

			roleClaim.Should().NotBeNull();
		}

        #endregion

        #region TSRAdmin

        private async Task<ApplicationUser> GetTSRAdmin() =>
			await GetEntity<ApplicationUser>(s => s.UserName == "TSRAdmin");

		[Test]
		public async Task TSRAdminRole()
		{
			var superAdmin = await GetTSRAdmin();
			var superAdminRole = await GetEntity<ApplicationRole>(s =>
				s.NormalizedName == ApplicationClaimValues.Administrator.ToUpper());
			superAdminRole.Should().NotBeNull();

			var userRole = await GetEntity<ApplicationUserRole>(s =>
				s.RoleId == superAdminRole.Id &&
				s.UserId == superAdmin.Id);

			userRole.Should().NotBeNull();
		}

		[Test]
		public async Task TSRAdminUser()
		{
			(await GetTSRAdmin()).Should().NotBeNull();
		}

		[Test]
		public async Task TSRAdminClaims()
		{
			var superAdmin = await GetTSRAdmin();

			var roleClaim = await GetEntity<ApplicationUserClaim>(s =>
				s.UserId == superAdmin.Id &&
				s.ClaimType == ClaimTypes.Role);

			roleClaim.Should().NotBeNull();
		}

		#endregion
	}
}
