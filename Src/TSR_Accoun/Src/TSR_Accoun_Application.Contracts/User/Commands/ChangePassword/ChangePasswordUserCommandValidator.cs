using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSR_Accoun_Application.Contracts.User.Commands.ChangePassword
{
	public class ChangePasswordUserCommandValidator : AbstractValidator<ChangePasswordUserCommand>
	{
		public ChangePasswordUserCommandValidator()
		{
			RuleFor(x => x.OldPassword).NotEmpty();
			RuleFor(x => x.NewPassword).NotEmpty();
			RuleFor(s => s.NewPassword).Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$").WithMessage("Password must have at least one alphabetical character, one digit, and be at least 8 characters long.");
			RuleFor(x => x.ConfirmPassword).NotEmpty()
		.Equal(x => x.NewPassword)
		.WithMessage("Passwords do not math.");

		}
	}
}
