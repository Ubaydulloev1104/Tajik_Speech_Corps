using Application.Contracts.Word.Responses;
using Application.Contracts.WordCategore.Commands.CreateWordCategory;
using Application.Contracts.WordCategore.Commands.DeleteWordCategory;
using Application.Contracts.WordCategore.Commands.UpdateWordCategory;
using Application.Contracts.WordCategore.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TSR_Client.Services.CategoryServices
{
    public interface ICategoryService
    {
        List<CategoryResponse> Category { get; set; }
        UpdateWordCategoryCommand updatingEntity { get; set; }
        DeleteWordCategoryCommand deletingEntity { get; set; }
        CreateWordCategoryCommand creatingEntity { get; set; }
        Task<List<CategoryResponse>> GetAllCategory();
        Task OnSaveUpdateClick();
        Task OnDeleteClick(string slug);
        Task OnSaveCreateClick();
        void OnUpdateClick(CategoryResponse updateEntity);

        Task<List<WordCategoriesResponse>> GetWordCategories();
        Task<List<WordCategoriesResponse>> GetWordCategoriesSinceCheckDate();
    }
}
