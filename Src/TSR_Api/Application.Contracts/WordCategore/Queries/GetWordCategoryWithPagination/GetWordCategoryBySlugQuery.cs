using Application.Contracts.WordCategore.Responses;

namespace Application.Contracts.WordCategore.Queries.GetWordCategoryWithPagination
{
    public class GetWordCategoryBySlugQuery : IRequest<CategoryResponse>
    {
        public string Slug { get; set; }
    }
}
