using Application.Contracts.WordCategore.Responses;

namespace Application.Contracts.Word.Responses
{
    public class WordCategoriesResponse
    {
        public Guid CategoryId { get; set; }
        public CategoryResponse Category { get; set; }
        public bool Selected { get; set; }
        public int WordCount { get; set; }
    }
}
