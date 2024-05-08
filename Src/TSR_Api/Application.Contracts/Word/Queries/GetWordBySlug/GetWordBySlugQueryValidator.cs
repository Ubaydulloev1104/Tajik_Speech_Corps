namespace Application.Contracts.Word.Queries.GetWordBySlug;

public class GetWordBySlugQueryValidator : AbstractValidator<GetWordBySlugQuery>
{
    public GetWordBySlugQueryValidator()
    {
        RuleFor(x => x.Slug).NotEmpty();
    }
}
