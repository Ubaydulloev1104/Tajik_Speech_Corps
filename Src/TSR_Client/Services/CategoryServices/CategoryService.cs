using Application.Contracts.Common;
using Application.Contracts.Word.Responses;
using Application.Contracts.WordCategore.Commands.CreateWordCategory;
using Application.Contracts.WordCategore.Commands.DeleteWordCategory;
using Application.Contracts.WordCategore.Commands.UpdateWordCategory;
using Application.Contracts.WordCategore.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TSR_Client.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }
        public List<CategoryResponse> Category { get; set; }
        public UpdateWordCategoryCommand updatingEntity { get; set; }
        public DeleteWordCategoryCommand deletingEntity { get; set; }
        public CreateWordCategoryCommand creatingEntity { get; set; }
        public async Task<List<CategoryResponse>> GetAllCategory()
        {
            var result = await _http.GetFromJsonAsync<PagedList<CategoryResponse>>("categories");
            Category = result.Items;
            creatingEntity = new() { Name = "" };
            return result.Items;
        }
        public void OnUpdateClick(CategoryResponse updateEntity)
        {
            updatingEntity = new()
            {
                Slug = updateEntity.Slug,
                Name = updateEntity.Name
            };
        }
        public async Task OnSaveUpdateClick()
        {
            var result = await _http.PutAsJsonAsync($"categories/{updatingEntity.Slug}", updatingEntity);
            result.EnsureSuccessStatusCode();
            updatingEntity = null;
            var result2 = await _http.GetFromJsonAsync<PagedList<CategoryResponse>>($"categories");
            Category = result2.Items;
        }
        public async Task OnDeleteClick(string slug)
        {
            await _http.DeleteAsync($"categories/{slug}");
            var result = await _http.GetFromJsonAsync<PagedList<CategoryResponse>>($"categories");
            Category = result.Items;
        }
        public async Task OnSaveCreateClick()
        {
            if (creatingEntity is not null)
                await _http.PostAsJsonAsync("categories", creatingEntity);
            creatingEntity.Name = string.Empty;
            var result = await _http.GetFromJsonAsync<PagedList<CategoryResponse>>($"categories");
            Category = result.Items;
        }

        public async Task<List<WordCategoriesResponse>> GetWordCategories()
        {
            var responce = await _http.GetFromJsonAsync<List<WordCategoriesResponse>>("categories/Word");
            return responce;
        }

        public async Task<List<WordCategoriesResponse>> GetWordCategoriesSinceCheckDate()
        {
            var responce = await _http.GetFromJsonAsync<List<WordCategoriesResponse>>("categories/Word");
            return responce;
        }
    }
}
