namespace Application.Contracts.WordCategore.Commands.DeleteWordCategory
{
    public class DeleteWordCategoryCommandValidator : AbstractValidator<DeleteWordCategoryCommand>
    {
        public DeleteWordCategoryCommandValidator()
        {
            RuleFor(x => x.Slug).NotEmpty();
        }
    }
}
