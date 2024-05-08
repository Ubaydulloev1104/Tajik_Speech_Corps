using Application.Contracts.Common;
using Application.Contracts.Word.Commands.Create;
using Application.Contracts.Word.Commands.Update;
using Application.Contracts.Word.Responses;
using Application.Contracts.WordCategore.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TSR_Client.Services.WordService
{
    public class WordService : IWordService
    {
        private readonly HttpClient _http;
        public WordService(HttpClient http)
        {
            _http = http;
            guidId = "";
            creatingNewWord = new()
            {
                Value="",
                Description = "",
                CategoryId = Guid.NewGuid(),
                CreateDate = DateTime.Now,
               
            };
        }

        public string guidId { get; set; } = string.Empty;
        public int PagesCount { get; set; }
        public List<CategoryResponse> Categories { get; set; }
        public List<WordListDto> Words { get; set; }
        public int FilteredVacanciesCount { get; set; } = 0;
        public CreateWordCommand creatingNewWord { get; set; }

        const float PageSize = 10f;
        public event Action OnChange;

        public async Task<List<CategoryResponse>> GetAllCategory()
        {
            var result = await _http.GetFromJsonAsync<PagedList<CategoryResponse>>("categories");
            Categories = result.Items;
            return Categories;
        }

        public async Task<List<WordListDto>> GetAllWord()
        {
            var result = await _http.GetFromJsonAsync<PagedList<WordListDto>>("words");
            Words = result.Items;
            return Words;
        }

        public async Task<List<WordListDto>> GetAllWords()
        {
            var result = await _http.GetFromJsonAsync<PagedList<WordListDto>>($"words?PageSize={int.MaxValue}");
            return result.Items;
        }

        public async Task<WordDetailsDto> GetBySlug(string slug)
        {
            var result = await _http.GetFromJsonAsync<WordDetailsDto>($"word/{slug}");
            return result;
        }

        public async Task<List<WordListDto>> GetFilteredWords(string Value = "", string categoryName = "All categories", int page = 1)
        {
            PagedList<WordListDto> result;
            if (Value == "")
            {
                if (categoryName == "All categories")
                {
                    if (page == 1)
                    {
                        FilteredVacanciesCount = (await _http.GetFromJsonAsync<PagedList<WordListDto>>($"words?PageSize={int.MaxValue}")).TotalCount;
                        PagesCount = (int)Math.Ceiling(FilteredVacanciesCount / PageSize);
                    }
                    result = await _http.GetFromJsonAsync<PagedList<WordListDto>>($"words?PageSize=10&Page={page}");
                }
                else
                {
                    if (page == 1)
                    {
                        FilteredVacanciesCount = (await _http.GetFromJsonAsync<PagedList<WordListDto>>($"words?Filters=Category@={categoryName}&PageSize={int.MaxValue}")).TotalCount;
                        PagesCount = (int)Math.Ceiling(FilteredVacanciesCount / PageSize);
                    }
                    result = await _http.GetFromJsonAsync<PagedList<WordListDto>>($"words?Filters=Category@={categoryName}&PageSize=10&Page={page}");
                }
            }
            else
            {
                if (page == 1)
                {
                    FilteredVacanciesCount = (await _http.GetFromJsonAsync<PagedList<WordListDto>>($"words?Filters=Value@={Value}&PageSize={int.MaxValue}")).TotalCount;
                    PagesCount = (int)Math.Ceiling(FilteredVacanciesCount / PageSize);
                }

                result = await _http.GetFromJsonAsync<PagedList<WordListDto>>($"words?Filters=Value@={Value}&PageSize=10&Page={page}");
            }
            OnChange?.Invoke();
            return result.Items;
        }

        public async Task<List<WordListDto>> GetWords()
        {
            throw new NotImplementedException();
        }

        public async Task<List<WordListDto>> GetWordByValue(string Value)
        {
            var result = await _http.GetFromJsonAsync<PagedList<WordListDto>>($"words?Filters=Value@={Value}");
            Words = result.Items;
            OnChange.Invoke();
            return Words;
        }

        public async Task OnDelete(string slug)
        {
            await _http.DeleteAsync($"words/{slug}");
        }

        public async Task<HttpResponseMessage> OnSaveCreateClick()
        {
			return await _http.PostAsJsonAsync("words", creatingNewWord);
		}

        public async Task UpdateWords(string slug)
        {
            var update = new UpdateWordCommand
            {
                Slug = slug,
                Value=creatingNewWord.Value,
                UpdatedDate = DateTime.Now,
                Description = creatingNewWord.Description,
                CategoryId = creatingNewWord.CategoryId, 
            };
            await _http.PutAsJsonAsync($"words/{slug}", update);
        }
    }
}
