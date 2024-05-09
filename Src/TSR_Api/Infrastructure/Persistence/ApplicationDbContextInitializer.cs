using Application.Contracts.Word;
using Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;
public class ApplicationDbContextInitializer(ApplicationDbContext dbContext, IConfiguration configuration)
{
    public async Task SeedAsync()
    {
        await CreateNoWord("NoWord");

        if (configuration["Environment"] != "Production")
        {
            await CreateWordAsync();
            await CreateApplicationsForAllOpenVacanciesAsync();
        }
    }

    private async Task CreateNoWord(string vacancyTitle)
    {
        var noCategory = await dbContext.Categories.FirstOrDefaultAsync(c => c.Slug == CommonWordSlugs.NoWordSlug);
        if (noCategory == null)
        {
            var category = new WordCategory { Name = "NoWord", Slug = CommonWordSlugs.NoWordSlug };
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
        var category = await CreateWordCategoryAsync("IT Specialists", "it-specialists");

        if (await dbContext.Words.FirstOrDefaultAsync(x => x.Slug == "devops-engineer") == null)
            await dbContext.Words.AddAsync(new Words
            {
                Slug = "devops-engineer",
                Value = "DevOps Engineer",
                Description =
                    "Не просто DevOps-инженер, а человек всесторонний, который с лёгкостью найдёт проблему и решит её, мыслит наперёд и предсказывает ошибки в будущем.\n\nУзнали в описании себя? Подайте заявку и станьте частью крутой финтех команды — Alif\n\nТребования:\n\nЗнания Linux (CentOS, Debian);\nПонимание работы с Linux в целом: настройка сети, устройство ФС, права доступа;\nУверенное знание командной строки и базовых утилит;\nУмение развернуть готовый стек (Python, PHP, Ruby, DB) для работы в production;\nУмение писать скрипты (sh, bash);\nMySQL, опыт написания простых запросов;\nБазовые знания docker.\nБудет плюсом:\n\nАnsible;\nGitLab;\nЗнание любой из систем мониторинга: zabbix, prometheus и сопутствующих утилит;\nОпыт работы с брокерами сообщений.\nДля нас самое ценное:\n\nЧестность и скромность;\nОтветственность и пунктуальность;\nУсердие в саморазвитии и в работе.\nПредлагаем:\n\nКарьерный рост;\nДружелюбный коллектив;\nКомфортный офис;\nВозможность развития вместе с компанией.\nЕсли Вы хотите участвовать в интересных проектах, работать в комфортных условиях и готовы стать частью нашей команды - ждем ваших резюме!",
                CreateDate = DateTime.Today.AddDays(-10),
                UpdatedDate = DateTime.Today.AddDays(30),
                CategoryId = category.Id,
            });

        if (await dbContext.Words.FirstOrDefaultAsync(x => x.Slug == "backend-developer") == null)
            await dbContext.Words.AddAsync(new Words
            {
                Slug = "backend-developer",
                Value= "Backend Developer",
                Description =
                    "As a Backend Developer, you will be responsible for server-side web application logic and integration of the front-end part...",
                CreateDate = DateTime.Today.AddDays(-15),
                UpdatedDate = DateTime.Today.AddDays(20),
                CategoryId = category.Id,
            });

        if (await dbContext.Words.FirstOrDefaultAsync(x => x.Slug == "frontend-developer") == null)
            await dbContext.Words.AddAsync(new Words
            {
                Slug = "frontend-developer",
                Value = "Frontend Developer",
                Description =
                    "In this role, you will be responsible for developing and implementing user interface components...",
                CreateDate = DateTime.Today.AddDays(-30),
                UpdatedDate = DateTime.Today.AddDays(-1),
                CategoryId = category.Id,
            });


        await dbContext.SaveChangesAsync();
    }

    

    private async Task CreateApplicationsAsync()
    {
        var word = await dbContext.Words
            .FirstOrDefaultAsync(x => x.Slug == "backend-developer");

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
        // Ensure dbContext is not null
        if (dbContext == null)
            throw new NullReferenceException("ApplicationDbContext is not initialized.");

        // List of users who will apply for all open vacancies
        var users = new List<string> { "applicant1", "Jerry" };

        // Get all open job vacancies
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
                    commit = "I am very interested in this position.",
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
