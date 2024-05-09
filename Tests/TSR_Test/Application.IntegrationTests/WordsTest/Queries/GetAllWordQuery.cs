using Application.Contracts.Common;
using Application.Contracts.Word.Responses;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;

namespace Application.IntegrationTests.WordsTest.Queries;
public class GetAllWordQuery : Testing
{
    private WordContext _context;

    [SetUp]
    public void SetUp()
    {
        _context = new WordContext();
    }


    [Test]
    public async Task GetAllJobVacancyQuery_ReturnsJobVacancies()
    {

        await _context.GetWord("Word1");
        await _context.GetWord("Word2");

        //Act
        var response = await _httpClient.GetAsync("/api/words");

        //Assert
        Assert.That(HttpStatusCode.OK == response.StatusCode);
        
        var jobVacancies = await response.Content.ReadFromJsonAsync<PagedList<WordListDto>>();
        Assert.That(jobVacancies, Is.Not.Null);
        Assert.That(jobVacancies.Items, Is.Not.Empty);
    }
}
