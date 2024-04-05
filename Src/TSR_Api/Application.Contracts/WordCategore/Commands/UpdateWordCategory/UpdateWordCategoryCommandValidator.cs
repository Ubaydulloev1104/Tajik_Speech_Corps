namespace Application.Contracts.WordCategore.Commands.UpdateWordCategory
{
    public class UpdateWordCategoryCommandValidator : AbstractValidator<UpdateWordCategoryCommand>
    {
        public UpdateWordCategoryCommandValidator()
        {
            RuleFor(s => s.Slug).NotEmpty();
            RuleFor(s => s.Name).NotEmpty();
        }
    }
}
