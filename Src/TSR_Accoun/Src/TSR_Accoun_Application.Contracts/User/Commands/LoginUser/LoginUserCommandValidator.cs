using FluentValidation;

namespace TSR_Accoun_Application.Contracts.User.Commands.LoginUser
{
	public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
	{
		public LoginUserCommandValidator()
		{
			RuleFor(s => s.Username).NotEmpty();
			RuleFor(s => s.Password).NotEmpty();
		}
	}
}
