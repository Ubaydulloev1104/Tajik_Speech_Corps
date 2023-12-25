using System.Security.Claims;

namespace TSR_Accoun_Application.Common.Interfaces.Services
{
	public interface IJwtTokenService
	{
		public string CreateTokenByClaims(IList<Claim> user, out DateTime expireDate);
		public string CreateRefreshToken(IList<Claim> user);
	}
}
