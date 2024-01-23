using System.Net.Http.Json;
using TSR_Accoun_Application.Contracts.Profile;
using TSR_Accoun_Application.Contracts.Profile.Commands.UpdateProfile;

namespace Application.IntegrationTests.UserProfile
{
	public class UpdateUserProfileTest : BaseTest
	{
		[Test]
		public async Task UpdateUserProfile_ShouldUpdateUserProfile_Success()
		{
			await AddApplicantAuthorizationAsync();
			var profile = new UpdateProfileCommand()
			{
				FirstName = "FirstName",
				LastName = "LastName",
				Email = Applicant.Email,
				Gender = Gender.Male,
				DateOfBirth = DateTime.Now,
				PhoneNumber = "+992125456789",
				AboutMyself = "ekmden ehfisuhfshfsfehf fjshefsjhf slufh self"

			};
			var response = await _client.PutAsJsonAsync("/api/Profile", profile);

			response.EnsureSuccessStatusCode();
		}
	}
}
