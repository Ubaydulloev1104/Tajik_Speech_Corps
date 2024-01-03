using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSR_Accoun_Application.Contracts.User.Commands.ChangePassword;
using TSR_Accoun_Application.Contracts.User.Commands.LoginUser;
using TSR_Accoun_Application.Contracts.User.Commands.RegisterUser;
using TSR_Accoun_Application.Contracts.User.Queries;

namespace TSR_Accoun_Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly ISender _mediator;
	

		public AuthController(ISender mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
		{
			var result = await _mediator.Send(request);
			return Ok(result);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterUserCommand request)
		{
			var result = await _mediator.Send(request);
			return Ok(result);
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> Refresh(GetAccessTokenUsingRefreshTokenQuery request)
		{
			var response = await _mediator.Send(request);
			return Ok(response);
		}


		[HttpPut("ChangePassword")]
		[Authorize]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordUserCommand command)
		{
			var response = await _mediator.Send(command);
			return Ok(response);
		}
	}
}
