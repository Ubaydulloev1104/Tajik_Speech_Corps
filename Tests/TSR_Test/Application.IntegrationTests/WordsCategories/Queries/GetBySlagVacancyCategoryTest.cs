using Application.Contracts.WordCategore.Queries.GetWordCategoryWithPagination;
using Application.IntegrationTests.WordsCategories.GetCreate;
using NUnit.Framework;
using System.Net;

namespace Application.IntegrationTests.WordsCategories.Queries
{
    public class GetBySlagVacancyCategoryTest : Testing
    {
        private CategoryContext _categoryContext;

        [SetUp]
        public void SetUp()
        {
            _categoryContext = new CategoryContext();
        }

        [Test]
        public async Task GetVacancyCategoryBySlug_Returns_NotFound()
        {
            //Arrange 
            var query = new GetWordCategoryBySlugQuery { Slug = "ui" };

            //Act
            var response = await _httpClient.GetAsync($"/api/categories/{query.Slug}");

            //Assert 
            Assert.That(HttpStatusCode.NotFound == response.StatusCode);
        }

        [Test]
        public async Task GetVacancyCategoryBySlug_Returs_StatusOK()
        {
            //Arrange 
            var query = new GetWordCategoryBySlugQuery { Slug = (await _categoryContext.GetCategory("ux")).Slug };

            //Act
            var response = await _httpClient.GetAsync($"/api/categories/{query.Slug}");

            //Assert 
            Assert.That(HttpStatusCode.OK == response.StatusCode);
            
        }
    }
}
