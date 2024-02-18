using FluentValidation;

namespace Application.Contracts.Applications.Commands.Delete
{
    public class DeleteApplicationCommandValidator : AbstractValidator<DeleteApplicationCommand>
    {
        public DeleteApplicationCommandValidator()
        {
            RuleFor(x => x.Slug).NotEmpty();
        }
    }
}
