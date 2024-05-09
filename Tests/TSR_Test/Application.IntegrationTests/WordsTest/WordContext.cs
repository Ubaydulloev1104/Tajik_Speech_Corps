using Application.IntegrationTests.WordsCategories.GetCreate;
using Domain.Entities;

namespace Application.IntegrationTests.WordsTest;

public class WordContext : Testing
{
    public async Task<Words> GetWord(string title)
    {
        var category = new CategoryContext();
        var word = await FindFirstOrDefaultAsync<Words>(j => j.Value == title);
        if (word != null)
            return word;

        var newWord = new Words
        {
            Value = title,
            Description = "Description",
            CreateDate = DateTime.Now,
            UpdatedDate = DateTime.Now.AddDays(2),
            CategoryId = await category.GetCategoryId("newWord"),
        };
        newWord.Slug = newWord.Value.ToLower().Replace(" ", "-");

        await AddAsync(newWord);

        return newWord;
    }
}
