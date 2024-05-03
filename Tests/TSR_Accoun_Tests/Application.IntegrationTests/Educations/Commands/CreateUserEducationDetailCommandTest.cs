using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Net;
using TSR_Accoun_Application.Contracts.Educations.Command.Create;

namespace Application.IntegrationTests.Educations.Commands
{
    public class CreateUserEducationDetailCommandTest : BaseTest
    {
        [Test]
        public async Task CreateUserEducationDetailCommand_ShouldCreateEducationDetailCommand_Success()
        {
            await AddApplicantAuthorizationAsync();

            var command = new CreateEducationDetailCommand()
            {
                Speciality = "test test",
                University = "test",
                StartDate = DateTime.Now.AddYears(-5),
                EndDate = DateTime.Now.AddYears(-1),
                UntilNow = false,
            };

            var response = await _client.PostAsJsonAsync("/api/Profile/CreateEducationDetail", command);
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task CreateUserEducationDetailCommand_ShouldCreateEducationDetailCommand_Duplicate()
        {
            await AddApplicantAuthorizationAsync();

            var command = new CreateEducationDetailCommand()
            {
                Speciality = "IT Security",
                University = "Khujand University",
                StartDate = DateTime.Now.AddYears(-5),
                EndDate = DateTime.Now.AddYears(-1),
                UntilNow = false,
            };


            var response = await _client.PostAsJsonAsync("/api/Profile/CreateEducationDetail", command);
            Assert.That(HttpStatusCode.OK == response.StatusCode);
            command.Speciality = "It security";
            command.University = "khujand university";
            response = await _client.PostAsJsonAsync("/api/Profile/CreateEducationDetail", command);

            Assert.That(HttpStatusCode.BadRequest == response.StatusCode);
            Assert.That((await response.Content.ReadFromJsonAsync<ProblemDetails>()).Detail.Contains("Education detail already exists"), Is.Not.False);
        }
    }
}
