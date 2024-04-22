using TSR_Accoun_Application.Contracts.Profile.Responses;

namespace Application.Common.Interfaces
{
    public interface IidentityService
    {
        Task<UserProfileResponse> ApplicantDetailsInfo(string userName = null);
    }
}
