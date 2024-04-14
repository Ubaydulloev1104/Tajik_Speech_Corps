namespace Application.Contracts.Applications.Commands.UpdateApplicationStatus
{
    public class UpdateApplicationStatusCommandValidator : AbstractValidator<UpdateApplicationStatuss>
    {
        public UpdateApplicationStatusCommandValidator()
        {
            RuleFor(v => v.Slug)
                .NotEmpty();
            RuleFor(v => v.StatusId)
                .NotEmpty();
        }
    }
}
