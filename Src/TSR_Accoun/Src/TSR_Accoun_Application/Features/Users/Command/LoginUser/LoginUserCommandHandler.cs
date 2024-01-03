using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSR_Accoun_Application.Common.Interfaces.Services;
using TSR_Accoun_Application.Contracts.User.Commands.LoginUser;
using TSR_Accoun_Application.Contracts.User.Responses;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.Users.Command.LoginUser
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, JwtTokenResponse>
	{
		private readonly IJwtTokenService _jwtTokenService;
		private readonly UserManager<ApplicationUser> _userManager;

		public LoginUserCommandHandler(IJwtTokenService jwtTokenService, UserManager<ApplicationUser> userManager)
		{
			_jwtTokenService = jwtTokenService;
			_userManager = userManager;
		}

		public async Task<JwtTokenResponse> Handle(LoginUserCommand request,
			CancellationToken cancellationToken)
		{
			ApplicationUser user =
				await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);
			_ = user ?? throw new UnauthorizedAccessException("Username is not found.");

			bool success = await _userManager.CheckPasswordAsync(user, request.Password);

			if (success)
			{
				return new JwtTokenResponse
				{
					RefreshToken = _jwtTokenService.CreateRefreshToken(await _userManager.GetClaimsAsync(user)),
					AccessToken = _jwtTokenService.CreateTokenByClaims(await _userManager.GetClaimsAsync(user), out var expireDate),
					AccessTokenValidateTo = expireDate
				};
			}
			throw new UnauthorizedAccessException("Incorrect password.");
		}
	}
}
