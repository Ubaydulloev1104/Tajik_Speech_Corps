namespace Infrastructure.Persistence;
public class ApplicationDbContextInitializer(ApplicationDbContext dbContext)
{

    public async Task SeedAsync()
    {
        await CreateNoVacancy("NoWord");
    }

    private async Task CreateNoVacancy(string wordTitle)
    {
        var hiddenCategory = await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == "NoWord");
        if (hiddenCategory == null)
        {
            var category = new WordCategory() { Name = "NoWord", Slug = "no_word" };
            await dbContext.Categories.AddAsync(category);
            hiddenCategory = category;
        }

        var hiddenWords = await dbContext.Words.FirstOrDefaultAsync(hv => hv.Value == wordTitle);
        if (hiddenWords == null)
        {
            var words = new Words()
            {
                Id = Guid.NewGuid(),
                Value = wordTitle,
                CreateDate = DateTime.Now,
                UpdatedDate = new DateTime(2099, 12, 31),
                Description = "",
                Slug = "no_word",
                CategoryId = hiddenCategory.Id,
                CreatedAt = DateTime.Now,
               
            };

            await dbContext.Words.AddAsync(words);
        }

        await dbContext.SaveChangesAsync();
    }
}
