using Application.Contracts.Word.Responses;

namespace Application.Contracts.Word.Queries.GetWordCategories
{
    public class GetWordCategoriesQuery : IRequest<List<WordCategoriesResponse>>
    {
        public bool CheckDate { get; set; } = true;
    }
}
