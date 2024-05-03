using System.Net.Http.Json;
using TSR_Accoun_Application.Contracts.UserRoles.Commands;
using TSR_Accoun_Domain.Entities;
using TSR_Accoun_Application;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Application.IntegrationTests.UserRoles.Commands
{
    public class CreateUserRolesCommandTest : BaseTest
    {
        private ApplicationUser _user = null!;
        [SetUp]
        public async Task SetUp()
        {
            _user = new ApplicationUser { UserName = "Test123", Email = "Test1231231@con.ty", };
            await AddUser(_user, "fasdfasdf@1231Q!");
        }

        [Test]
        public async Task CreateUserRole_ShouldCreateUserRole_Success()
        {
            var role = new ApplicationRole { Slug = "rol1", Name = "rol1", NormalizedName = "ROL1" };

            await AddEntity(role);

            var command = new CreateUserRolesCommand { UserName = _user.UserName, RoleName = role.Name };

            await AddAuthorizationAsync();
            var response = await _client.PostAsJsonAsync("/api/userRoles", command);

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task CreateUserRole_UnExistUser_ShouldReturnNotFound()
        {
            var role = new ApplicationRole { Slug = "rol3", Name = "Rol3", NormalizedName = "ROL3" };

            await AddEntity(role);
            var command = new CreateUserRolesCommand { UserName = _user.UserName + "afdsdf", RoleName = role.Name };

            await AddAuthorizationAsync();
            var response = await _client.PostAsJsonAsync("/api/UserRoles", command);

            Assert.That((int)response.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }
    }
}
