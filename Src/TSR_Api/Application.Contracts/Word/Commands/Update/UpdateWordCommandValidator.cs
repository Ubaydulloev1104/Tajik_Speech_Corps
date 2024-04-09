namespace Application.Contracts.Word.Commands.Update
{
    public class UpdateWordCommandValidator : AbstractValidator<UpdateWordCommand>
    {
        public UpdateWordCommandValidator()
        {
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.UpdatedDate).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.RequiredYearOfExperience).GreaterThanOrEqualTo(0);
            RuleFor(x => x.WorkSchedule).IsInEnum();
        }
    }
}
