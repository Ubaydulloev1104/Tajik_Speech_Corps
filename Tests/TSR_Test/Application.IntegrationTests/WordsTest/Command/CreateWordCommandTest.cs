using Application.Contracts.Word.Commands.Create;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Net.Http.Json;

namespace Application.IntegrationTests.WordsTest.Command;

public class CreateWordCommandTest : Testing
{
    private static readonly Random Random = new();

    [Test]
    public async Task CreateWordCommand_CreatingWordWithQuestions_Success()
    {
        RunAsReviewerAsync();
        var word = new CreateWordCommand
        {
            Value = "New New New",
            Description = RandomString(10),
           
            CreateDate = DateTime.Now,
            UpdatedDate = DateTime.Now.AddDays(2),
            CategoryId = await AddWordCategory("newWord")
        };
        var response = await _httpClient.PostAsJsonAsync("/api/words", word);

        response.EnsureSuccessStatusCode();

        response.Should().NotBeNull();
    }
    [Test]
    public async Task CreateWordCommand_CreatingWordWithTask_Success()
    {
        RunAsReviewerAsync();
        var words = new CreateWordCommand
        {
            Value = "Test Word",
            Description = RandomString(10),
            CreateDate = DateTime.Now,
            UpdatedDate = DateTime.Now.AddDays(2),
            CategoryId = await AddWordCategory("newWord")
        };
        var response = await _httpClient.PostAsJsonAsync("/api/words", words);

        response.EnsureSuccessStatusCode();

        response.Should().NotBeNull();
    }

    [Test]
    public async Task CreateWord_ValidRequest_FillDatabase()
    {
        RunAsReviewerAsync();
        var jobVacancy = new CreateWordCommand
        {
            Value = "Cool Word",
            Description = RandomString(10),
            CreateDate = DateTime.Now,
            UpdatedDate = DateTime.Now.AddDays(2),
            CategoryId = await AddWordCategory("newWord1"),
           
        };
        await _httpClient.PostAsJsonAsync("/api/words", jobVacancy);

        var databaseVacancy = await FindFirstOrDefaultAsync<Words>(s =>
            s.CategoryId == jobVacancy.CategoryId &&
            s.Value == jobVacancy.Value &&
            s.Description == jobVacancy.Description);
        databaseVacancy.Should().NotBeNull();

        databaseVacancy.CreatedAt.Should().NotBe(null);
        databaseVacancy.CreatedBy.Should().NotBeEmpty();
    }


    private async Task<Guid> AddWordCategory(string name)
    {
        var wordCategory = new WordCategory
        {
            Name = name,
            Id = Guid.NewGuid(),
        };
        await AddAsync(wordCategory);
        return wordCategory.Id;
    }

    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}
