using Application.Contracts.Word.Commands.Update;
using NUnit.Framework;
using System.Net.Http.Json;

namespace Application.IntegrationTests.WordsTest.Command;

public class UpdateWordCommandTest : Testing
{

    [Test]
    public async Task UpdateJobVacancyCommand_UpdatingJobVacancyCommand_Success()
    {
        WordContext words = new WordContext();
        var word = await words.GetWord("newWord");

        var updateCommand = new UpdateWordCommand
        {
            Value = "Word Updated",
            Description = word.Description,
            UpdatedDate = word.UpdatedDate,
            CategoryId = word.CategoryId,
            Slug = word.Slug,
        };

        RunAsReviewerAsync();
        var response = await _httpClient.PutAsJsonAsync($"/api/words/{word.Slug}", updateCommand);

        response.EnsureSuccessStatusCode();

    }
}
