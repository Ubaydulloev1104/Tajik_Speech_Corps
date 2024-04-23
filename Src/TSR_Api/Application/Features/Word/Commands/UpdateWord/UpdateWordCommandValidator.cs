using Application.Contracts.Word.Commands.Update;

namespace Application.Features.Word.Commands.UpdateWord
{
    public class UpdateWordCommandValidator : AbstractValidator<UpdateWordCommand>
    {
        public UpdateWordCommandValidator()
        {
            RuleFor(x => x.Slug).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.UpdatedDate).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}
