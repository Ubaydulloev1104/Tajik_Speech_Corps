using MediatR;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TSR_Accoun_Application.Common.Interfaces.Services;
using TSR_Accoun_Application.Contracts.User.Queries;
using TSR_Accoun_Application.Contracts.User.Responses;

namespace TSR_Accoun_Application.Features.Users.Queries
{
	public class GetAccessTokenUsingRefreshTokenQueryHandler : IRequestHandler<GetAccessTokenUsingRefreshTokenQuery, JwtTokenResponse>
	{
		private readonly IJwtTokenService _tokenService;

		public GetAccessTokenUsingRefreshTokenQueryHandler(IJwtTokenService tokenService)
		{
			_tokenService = tokenService;
		}
		Task<JwtTokenResponse>
			IRequestHandler<GetAccessTokenUsingRefreshTokenQuery, JwtTokenResponse>.Handle(
				GetAccessTokenUsingRefreshTokenQuery request, CancellationToken cancellationToken)
		{
			if (!AreTokensRelated(request))
			{
				throw new ValidationException("Tokens are not related");
			}
			var claims = GetTokenClaims(request.AccessToken);
			if (claims.Count != 0)
			{
				return Task.FromResult(new JwtTokenResponse
				{
					AccessToken = _tokenService.CreateTokenByClaims(claims, out var expireDate),
					RefreshToken = _tokenService.CreateRefreshToken(claims),
					AccessTokenValidateTo = expireDate
				});
			}
			throw new ValidationException("Could not validate token");
		}
		private bool AreTokensRelated(GetAccessTokenUsingRefreshTokenQuery query)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			var refreshClaims = tokenHandler.ReadJwtToken(query.RefreshToken);
			var refreshUserId = refreshClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Id);
			var accessClaims = tokenHandler.ReadJwtToken(query.AccessToken);
			var accessUserId = accessClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Id);
			return refreshUserId != null &&
				   accessUserId != null &&
				   refreshUserId.Value == accessUserId.Value;
		}
		private List<Claim> GetTokenClaims(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			if (!tokenHandler.CanReadToken(token))
			{
				throw new ValidationException("Token is not valid! Can not read it");
			}

			var jwtToken = tokenHandler.ReadJwtToken(token);
			List<Claim> claims = jwtToken.Claims.ToList();

			return claims;
		}
	}
}
