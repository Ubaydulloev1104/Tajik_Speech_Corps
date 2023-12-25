using MediatR;
using TSR_Accoun_Application.Contracts.User.Responses;

namespace TSR_Accoun_Application.Contracts.User.Commands.LoginUser
{
	public class LoginUserCommand : IRequest<JwtTokenResponse>
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
