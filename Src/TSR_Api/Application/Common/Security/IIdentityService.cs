using Application.Contracts.Identity.Responces;

namespace Application.Common.Security
{
    public interface IIdentityService
    {
        Task<bool> HasPermissionAsync(Guid userId, string permission, CancellationToken cancellationToken);

        Task<UserIdentityResponse> GetUserIdentityAsync(Guid userId, CancellationToken cancellationToken);
    }
}
