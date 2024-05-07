using System.Security.Claims;

namespace Application.IntegrationTests.Common.Interfaces;

public interface IJwtTokenService
{
    public string CreateTokenByClaims(IList<Claim> user);
}
