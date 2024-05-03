using FluentAssertions;
using System.Net.Http.Json;
using System.Net;
using TSR_Accoun_Application.Contracts.Claim.Commands;
using TSR_Accoun_Domain.Entities;

namespace Application.IntegrationTests
{
	public class ClaimsTests : BaseTest
	{
		private async Task<Guid> CreateSampleUserAsync()
		{
			var user = new ApplicationUser
			{
				Id = Guid.NewGuid(),
				UserName = "username",
				NormalizedUserName = "USERNAME",
				Email = "email@gmail.com",
				NormalizedEmail = "EMAIL@GMAIL.COM",
				EmailConfirmed = false,
				PhoneNumber = "1234567",
				PhoneNumberConfirmed = false,
			};

			var users = await GetAll<ApplicationUser>();
			if (users.Count > 0)
			{
				user = users[0];
			}
			else
			{
				await AddUser(user, "A2dAsdf@");
			}

			return user.Id;
		}

		//create claim
		[Test]
		public async Task CreateClaim_ValidRequest_ReturnsOkResult()
		{
			var userId = await CreateSampleUserAsync();

			var createCommand = new CreateClaimCommand
			{
				UserId = userId,
				ClaimType = "Application",
				ClaimValue = "TSR_Accoun"
			};
			await AddAuthorizationAsync();
			HttpResponseMessage createResponse = await _client.PostAsJsonAsync("api/claims", createCommand);

			Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public async Task CreateClaim_InvalidUserIdRequest_ReturnsUserNotFound()
		{
			await CreateSampleUserAsync();

			var createCommand = new CreateClaimCommand
			{
				UserId = Guid.Empty,
				ClaimType = "Application",
				ClaimValue = "TSR_Accoun"
            };

			await AddAuthorizationAsync();

			HttpResponseMessage createResponse = await _client.PostAsJsonAsync("api/claims", createCommand);

			Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
			Assert.That(await createResponse.Content.ReadAsStringAsync(), Does.Contain("user is not found"));
		}
		//create claim

		//put claim
		[Test]
		public async Task Update_ValidRequest_ReturnsOkResult()
		{
			var userId = await CreateSampleUserAsync();

			var sampleClaim = new ApplicationUserClaim
			{
				Id = 0,
				UserId = userId,
				ClaimType = "Application",
				ClaimValue = "TSR.Test",
				Slug = "user1-application"
			};

			await AddEntity(sampleClaim);

			await AddAuthorizationAsync();

			var putCommand = new UpdateClaimCommand { ClaimValue = "TSR.Test", Slug = "user1-application" };
			HttpResponseMessage response = await _client.PutAsJsonAsync($"api/claims/{sampleClaim.Slug}", putCommand);

			response.StatusCode.Should().Be(HttpStatusCode.OK);

			ApplicationUserClaim updatedClaim = await GetEntity<ApplicationUserClaim>(s => s.Id == sampleClaim.Id);
			updatedClaim.ClaimValue.Should().Be(sampleClaim.ClaimValue);
		}

		//put claim


		//delete claim
		[Test]
		public async Task Delete_ValidRequest_ReturnsOkResult()
		{
			var userId = await CreateSampleUserAsync();

			var sampleClaim = new ApplicationUserClaim
			{
				Id = 0,
				UserId = userId,
				ClaimType = "Application",
				ClaimValue = "TSR.Test",
				Slug = "user1-application"
			};
			await AddAuthorizationAsync();

			await AddEntity(sampleClaim);

			HttpResponseMessage response = await _client.DeleteAsync($"api/Claims/{sampleClaim.Slug}");

			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		//delete claim
	}
}
