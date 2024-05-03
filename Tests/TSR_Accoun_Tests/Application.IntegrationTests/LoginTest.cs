using NUnit.Framework;
using System.Net.Http.Json;
using System.Net;
using TSR_Accoun_Application.Contracts.User.Commands.LoginUser;
using TSR_Accoun_Application.Contracts.User.Responses;

namespace Application.IntegrationTests
{
	[TestFixture]
	public class LoginTest : BaseTest
	{
		[Test]
		public async Task Login_RequestWithCorrectLoginData_ReturnsOk()
		{
			// Arrange
			var request = new LoginUserCommand { Username = "@Azamjon123", Password = "password@#12P" };

			// Act
			var response = await _client.PostAsJsonAsync("api/Auth/login", request);

			// Assert
			var jwt = await response.Content.ReadFromJsonAsync<JwtTokenResponse>();
			Assert.Multiple(() =>
			{
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
				Assert.That(jwt?.AccessToken, Is.Not.Null.Or.Empty);
			});
		}

		[Test]
		public async Task Login_RequestWithEmptyLoginData_ReturnsUnauthorized()
		{
			// Arrange
			var request = new LoginUserCommand { Username = "null", Password = "12345" };

			// Act
			var response = await _client.PostAsJsonAsync("/api/Auth/login", request);

			// Assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
		}

		[Test]
		[TestCase("@Alex22", "password")]
		[TestCase("@Alex22", "ejehfefhuehf")]
		[TestCase("@Alex", "fesijfwer11")]
		[TestCase("@Alex", "password@#12P")]
		public async Task Login_RequestWithIncorrectLoginData_ReturnsUnauthorized(string username, string password)
		{
			// Arrange
			var request = new LoginUserCommand { Username = username, Password = password };

			// Act
			var response = await _client.PostAsJsonAsync("api/Auth/login", request);

			// Assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
		}
	}
}
