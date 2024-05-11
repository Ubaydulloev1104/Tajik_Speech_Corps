using Application.Contracts.Word;
using Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;
public class ApplicationDbContextInitializer(ApplicationDbContext dbContext, IConfiguration configuration)
{
    public async Task SeedAsync()
    {
        //await CreateNoWord("Test");

        //if (configuration["Environment"] != "Production")
        //{
        //    await CreateWordAsync();
        //    await CreateApplicationsForAllOpenVacanciesAsync();
        //}
    }

    private async Task CreateNoWord(string vacancyTitle)
    {
        var noCategory = await dbContext.Categories.FirstOrDefaultAsync(c => c.Slug == CommonWordSlugs.NoWordSlug);
        if (noCategory == null)
        {
            var category = new WordCategory { Name = "Test", Slug = CommonWordSlugs.NoWordSlug };
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync(); // SaveChangesAsync after adding category
            noCategory = category;
        }

        var noWord = await dbContext.Words.FirstOrDefaultAsync(hv => hv.Slug == CommonWordSlugs.NoWordSlug);
        if (noWord == null)
        {
            var vacancy = new Words
            {
                Value = vacancyTitle,
                CreateDate = DateTime.Now,
                UpdatedDate = new DateTime(2099, 12, 31),
                Description = "",
                Slug = CommonWordSlugs.NoWordSlug,
                CategoryId = noCategory.Id,
                CreatedAt = DateTime.Now,
            };

            await dbContext.Words.AddAsync(vacancy);
            await dbContext.SaveChangesAsync(); // SaveChangesAsync after adding word
        }
    }

    private async Task<WordCategory> CreateWordCategoryAsync(string name, string slug)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Slug == slug && x.Name == name);
        if (category != null)
            return category;

        category = new WordCategory() { Name = name, Slug = slug };
        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();
        return category;
    }

    private async Task CreateWordAsync()
    {
        var category = await CreateWordCategoryAsync("New Word", "new-Word");

        if (await dbContext.Words.FirstOrDefaultAsync(x => x.Slug == "NEW Word") == null)
            await dbContext.Words.AddAsync(new Words
            {
                Slug = "new-Word",
                Value = "New Word",
                Description =
                    "Test test test",
                CreateDate = DateTime.Today.AddDays(-10),
                UpdatedDate = DateTime.Today.AddDays(30),
                CategoryId = category.Id,
            });
        await dbContext.SaveChangesAsync();
    }

    

    private async Task CreateApplicationsAsync()
    {
        var word = await dbContext.Words
            .FirstOrDefaultAsync(x => x.Slug == "new-word");

        if (word != null)
        {
            if (await dbContext.Applications
                    .FirstOrDefaultAsync(x => x.Slug == "applicant1-backend-developer") == null)
            {
                var application = new Domain.Entities.Application()
                {
                    WordId = word.Id,
                    commit = "test test test",
                    AppliedAt = DateTime.Now,
                    ApplicantUsername = "applicant1",
                    Slug = "applicant1-backend-developer",
                };
                await dbContext.Applications.AddAsync(application);
                await dbContext.ApplicationTimelineEvents.AddAsync(new ApplicationTimelineEvent
                {
                    ApplicationId = application.Id,
                    Note = "Applied",
                    EventType = TimelineEventType.Created,
                    Time = DateTime.Now,
                });
            }
        }

        var internship = await dbContext.Words
            .FirstOrDefaultAsync(x => x.Slug == "data-scientist-intern");
        if (internship != null)
        {
            if (await dbContext.Words.FirstOrDefaultAsync(x => x.Slug == "applicant1-data-scientist-intern") ==
                null)
            {
                var application2 = new Domain.Entities.Application()
                {
                    WordId = internship.Id,
                    commit = "test test test",
                    AppliedAt = DateTime.Now,
                    ApplicantUsername = "applicant1",
                    Slug = "applicant1-data-scientist-intern",
                 
                };
                await dbContext.Applications.AddAsync(application2);
                await dbContext.ApplicationTimelineEvents.AddAsync(new ApplicationTimelineEvent
                {
                    ApplicationId = application2.Id,
                    Note = "Applied",
                    EventType = TimelineEventType.Created,
                    Time = DateTime.Now,
                });
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private async Task CreateApplicationsForAllOpenVacanciesAsync()
    {
        
        var users = new List<string> { "applicant1", "Jerry" };
        var words = await dbContext.Words
            .Where(x => x.CreateDate > DateTime.Now)
            .ToListAsync();
        // Loop through each user
        foreach (var user in users)
        {
            // Apply for all open job vacancies
            foreach (var vacancy in words)
            {
                if (await dbContext.Applications.FirstOrDefaultAsync(x => x.Slug == $"{user}-{vacancy.Slug}") != null)
                    continue;

                var application = new Domain.Entities.Application()
                {
                    WordId = vacancy.Id,
                    commit = "Test",
                    AppliedAt = RandomDayFunc(vacancy.CreateDate, vacancy.UpdatedDate),
                    ApplicantUsername = user,
                    Slug = $"{user}-{vacancy.Slug}",

                };
                await dbContext.Applications.AddAsync(application);
                await dbContext.ApplicationTimelineEvents.AddAsync(new ApplicationTimelineEvent
                {
                    ApplicationId = application.Id,
                    Note = "Applied",
                    EventType = TimelineEventType.Created,
                    Time = DateTime.Now,
                });
            }
        }

        await dbContext.SaveChangesAsync(); // SaveChangesAsync after adding applications
    }

    readonly Random _rnd = new();
    DateTime RandomDayFunc(DateTime start, DateTime end)
    {
        int range = (end - start).Days;
        var randomDate = start.AddDays(_rnd.Next(range));
        var randomTime = new TimeSpan(_rnd.Next(0, 24), _rnd.Next(0, 60), _rnd.Next(0, 60));
        return randomDate + randomTime;
    }

}
