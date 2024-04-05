using Application.Contracts.WordCategore.Responses;

namespace Application.Contracts.WordCategore.Queries.GetVacancyCategorySlugId
{
    public class GetWordCategoryByIdQuery : IRequest<CategoryResponse>
    {
        public Guid Id { get; set; }
    }
}
