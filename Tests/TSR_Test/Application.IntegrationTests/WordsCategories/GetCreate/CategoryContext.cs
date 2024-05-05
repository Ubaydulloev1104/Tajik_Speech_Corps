using Domain.Entities;

namespace Application.IntegrationTests.WordsCategories.GetCreate
{
    public class CategoryContext : Testing
    {
        public async Task<Guid> GetCategoryId(string name)
        {
            var catogy = await FindFirstOrDefaultAsync<WordCategory>(c => c.Name == name);
            if (catogy != null)
                return catogy.Id;

            var newCategory = new WordCategory
            {
                Name = name,
                Slug = name.ToLower().Replace(" ", "-")
            };
            await AddAsync(newCategory);
            return newCategory.Id;
        }

        public async Task<WordCategory> GetCategory(string name)
        {
            var catogy = await FindFirstOrDefaultAsync<WordCategory>(c => c.Name == name);
            if (catogy != null)
                return catogy;

            var newCategory = new WordCategory
            {
                Name = name,
                Slug = name.ToLower().Replace(" ", "-")
            };
            await AddAsync(newCategory);
            return newCategory;
        }
    }

}
