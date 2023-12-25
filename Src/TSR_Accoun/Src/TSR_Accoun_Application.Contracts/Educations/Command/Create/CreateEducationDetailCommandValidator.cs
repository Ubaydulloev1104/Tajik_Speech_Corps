using FluentValidation;

namespace TSR_Accoun_Application.Contracts.Educations.Command.Create
{
	public class CreateEducationDetailCommandValidator : AbstractValidator<CreateEducationDetailCommand>
	{
		public CreateEducationDetailCommandValidator()
		{
			RuleFor(e => e.University).NotEmpty().MinimumLength(3);
			RuleFor(e => e.Speciality).NotEmpty().MinimumLength(5);
			RuleFor(e => e.StartDate).NotEmpty();
		}
	}
}
