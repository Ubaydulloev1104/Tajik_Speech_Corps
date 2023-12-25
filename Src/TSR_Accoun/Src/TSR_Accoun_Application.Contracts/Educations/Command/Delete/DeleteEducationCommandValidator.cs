using FluentValidation;

namespace TSR_Accoun_Application.Contracts.Educations.Command.Delete
{
	public class DeleteEducationCommandValidator : AbstractValidator<DeleteEducationCommand>
	{
		public DeleteEducationCommandValidator()
		{
			RuleFor(e => e.Id).NotEmpty();
		}
	}
}
