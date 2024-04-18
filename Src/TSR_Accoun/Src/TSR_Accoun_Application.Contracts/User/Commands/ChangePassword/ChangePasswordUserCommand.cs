using MediatR;

namespace TSR_Accoun_Application.Contracts.User.Commands.ChangePassword
{
    public class ChangePasswordUserCommand : IRequest<bool>
	{
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
