namespace Application.Contracts.Word.Commands.Delete
{
    public class DeleteWordCommandValidator : AbstractValidator<DeleteWordCommand>
    {
        public DeleteWordCommandValidator()
        {
            RuleFor(x => x.Slug).NotEmpty();
        }
    }
}
