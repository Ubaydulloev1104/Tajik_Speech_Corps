using NUnit.Framework;
using System.Net.Http.Json;
using System.Net;
using Domain.Entities;
using Application.Contracts.WordCategore.Commands.UpdateWordCategory;

namespace Application.IntegrationTests.WordsCategories.Commands
{
    public class UpdateVacancyCategoryCommandTest : Testing
    {
        [Test]
        public async Task UpdateVacancyCategoryCommand_ShouldUpdateVacancyCategoryCommand_Success()
        {
            var category = new WordCategory { Name = "Test", Slug = "test" };
            await AddAsync(category);

            var updateCategory = new UpdateWordCategoryCommand { Name = "Test2", Slug = category.Slug };
            RunAsReviewerAsync();
            var response = await _httpClient.PutAsJsonAsync($"/api/categories/{category.Slug}", updateCategory);

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task UpdateVacancyCategoryCommand_ReturnsNotFound()
        {
            var updateCategory = new UpdateWordCategoryCommand { Name = "Test2", Slug = "test2" };

            RunAsReviewerAsync();
            var response = await _httpClient.PutAsJsonAsync($"/api/categories/{updateCategory.Slug}", updateCategory);
            Assert.That(HttpStatusCode.NotFound == response.StatusCode);
        }
    }
}
