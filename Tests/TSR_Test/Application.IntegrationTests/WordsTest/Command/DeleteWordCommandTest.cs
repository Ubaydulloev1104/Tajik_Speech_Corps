using NUnit.Framework;

namespace Application.IntegrationTests.WordsTest.Command;

public class DeleteWordCommandTestDeleteJobVacancyCommandTest : Testing
{
    [Test]
    public async Task DeleteWordCommand_ShouldDeleteWordCommand_Success()
    {
        var word = new WordContext();
        RunAsReviewerAsync();
        var response = await _httpClient.DeleteAsync($"/api/words/{(await word.GetWord("Word")).Slug}");

        response.EnsureSuccessStatusCode();
    }
}
