using NUnit.Framework;
using System.Net.Http.Json;
using System.Net;
using Application.Contracts.WordCategore.Commands.CreateWordCategory;

namespace Application.IntegrationTests.WordsCategories.Commands
{
    public class CreateWordCategoryCommandTest : Testing
    {
        [Test]
        public async Task CreateVacancyCategoryCommand_ShouldCreateVacancyCategory_Success()
        {
            var category = new CreateWordCategoryCommand { Name = "Word" };

            RunAsAdministratorAsync();
            var response = await _httpClient.PostAsJsonAsync("/api/categories", category);

            Assert.That(HttpStatusCode.Created == response.StatusCode);
        }
    }
}
