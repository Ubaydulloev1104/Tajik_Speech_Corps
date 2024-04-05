namespace Application.Contracts.WordCategore.Commands.CreateWordCategory
{
    public class CreateWordCategoryCommandValidator : AbstractValidator<CreateWordCategoryCommand>
    {
        public CreateWordCategoryCommandValidator()
        {
            RuleFor(s => s.Name).NotEmpty();
        }
    }
}
