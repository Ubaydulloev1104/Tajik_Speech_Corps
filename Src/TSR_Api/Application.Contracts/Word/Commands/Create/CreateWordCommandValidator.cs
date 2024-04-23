namespace Application.Contracts.Word.Commands.Create
{
    public class CreateWordCommandValidator : AbstractValidator<CreateWordCommand>
    {
        public CreateWordCommandValidator()
        {
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}
