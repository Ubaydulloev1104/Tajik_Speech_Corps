using Application.Contracts.WordCategore.Queries.GetWordCategoryWithPagination;

namespace Application.Contracts.WordCategore.Queries.GetWordCategorySlugId
{
    public class GetWordCategoryBySlugQueryValidator : AbstractValidator<GetWordCategoryBySlugQuery>
    {
        public GetWordCategoryBySlugQueryValidator()
        {
            RuleFor(x => x.Slug).NotEmpty();
        }
    }
}
