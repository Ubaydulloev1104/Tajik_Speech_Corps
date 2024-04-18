using Application.Contracts.WordCategore.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Application.Contracts.Word.Responses;
using Application.Contracts.Word.Commands.Create;

namespace TSR_Client.Services.WordService
{
    public interface IWordService
    {
        event Action OnChange;
        string guidId { get; set; }
        public int PagesCount { get; set; }
        List<CategoryResponse> Categories { get; set; }
        List<WordListDto> Words { get; set; }
        //  CreateVacancyCategoryCommand creatingEntity { get; set; }
        public int FilteredVacanciesCount { get; set; }
        CreateWordCommand creatingNewWord { get; set; }
        Task<List<WordListDto>> GetAllWord();
        /* Renamed version of the upper method name with a typo */
        Task<List<WordListDto>> GetAllWords();
        Task<List<WordListDto>> GetFilteredWords(string Value = "", string categoryName = "All categories", int page = 1);
        Task<List<CategoryResponse>> GetAllCategory();

        Task<List<WordListDto>> GetWordByValue(string Value);
        Task OnSaveCreateClick();
        Task OnDelete(string slug);
        Task<List<WordListDto>> GetWords();
        Task UpdateWords(string slug);
        Task<WordDetailsDto> GetBySlug(string slug);
    }
}
