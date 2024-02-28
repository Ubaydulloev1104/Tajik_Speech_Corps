namespace Application.Contracts.Applications.Commands.CreateApplication
{
	public class CreateApplicationWithoutApplicantIdCommandValidator : AbstractValidator<CreateApplicationWithoutApplicantIdCommand>
	{
		public CreateApplicationWithoutApplicantIdCommandValidator()
		{
			RuleFor(v => v.WordId).NotEmpty();
		}
	}
}
