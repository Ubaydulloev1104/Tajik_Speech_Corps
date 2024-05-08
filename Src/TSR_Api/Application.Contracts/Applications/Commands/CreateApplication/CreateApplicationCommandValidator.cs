namespace Application.Contracts.Applications.Commands.CreateApplication;

public class CreateApplicationCommandValidator : AbstractValidator<CreateApplicationCommand>
{
	public CreateApplicationCommandValidator()
	{
		RuleFor(v => v.WordId).NotEmpty();
	}
}
