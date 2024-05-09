using Application.Contracts.Word.Queries.GetWordBySlug;
using Domain.Entities;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;

namespace Application.IntegrationTests.WordsTest.Queries;

public class GetWordBySlugQueryTest : Testing
{
    private WordContext _context;

    [SetUp]
    public void SetUp()
    {
        _context = new WordContext();
    }

    [Test]
    public async Task GetWordBySlugQuery_IfNotFound_ReturnNotFoundWordSlug()
    {
        // Arrange
        var query = new GetWordBySlugQuery { Slug = "c-sharp-developer" };

        // Act
        var response = await _httpClient.GetAsync($"/api/words/{query.Slug}");

        // Assert
        Assert.That(HttpStatusCode.NotFound == response.StatusCode);
    }

    [Test]
    public async Task GetWordBySlug_IfFound_ReturnWord()
    {
        //Arrange 
        var query = new GetWordBySlugQuery { Slug = (await _context.GetWord("Backend")).Slug };

        //Act
        var response = await _httpClient.GetAsync($"/api/words/{query.Slug}");

        //Assert
        Assert.That(HttpStatusCode.OK == response.StatusCode);
        var jobVacancy = await response.Content.ReadFromJsonAsync<Words>();
        Assert.That(query.Slug == jobVacancy.Slug);
    }
}
