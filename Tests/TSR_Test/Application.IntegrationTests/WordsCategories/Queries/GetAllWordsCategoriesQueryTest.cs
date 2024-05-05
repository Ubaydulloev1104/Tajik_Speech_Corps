using Application.Contracts.Common;
using Application.Contracts.WordCategore.Responses;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Net;
using Application.IntegrationTests.WordsCategories.GetCreate;

namespace Application.IntegrationTests.WordsCategories.Queries
{
    public class GetAllVacancyCategoriesQueryTest : Testing
    {
        private CategoryContext _categoryContext;

        [SetUp]
        public void SetUp()
        {
            _categoryContext = new CategoryContext();
        }

        [Test]
        public async Task GetAllVacancyCategoriesQuery_ReturnsInternshipVacancies()
        {
            //Arrange
            await _categoryContext.GetCategory("category0001");
            await _categoryContext.GetCategory("category0002");

            //Act
            var response = await _httpClient.GetAsync("/api/categories");

            //Assert
            Assert.That(HttpStatusCode.OK == response.StatusCode);
            var wordCategories = await response.Content.ReadFromJsonAsync<PagedList<CategoryResponse>>();
            Assert.That(wordCategories, Is.Not.Null);
            Assert.That(wordCategories.Items, Is.Not.Empty);
        }
    }

}
