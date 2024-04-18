using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TSR_Accoun_Application.Contracts.Educations.Command.Create;
using TSR_Accoun_Application.Contracts.Educations.Command.Update;
using TSR_Accoun_Application.Contracts.Educations.Responsess;
using TSR_Accoun_Application.Contracts.Profile.Commands.UpdateProfile;
using TSR_Accoun_Application.Contracts.Profile.Responses;

namespace TSR_Client.Services.Profile
{
    public interface IUserProfileService
    {
        Task<UserProfileResponse> Get(string userName = null);
        Task<string> Update(UpdateProfileCommand command);
        Task<List<UserEducationResponse>> GetEducationsByUser();
        Task<List<UserEducationResponse>> GetAllEducations();
        Task<HttpResponseMessage> CreateEducationAsуnc(CreateEducationDetailCommand command);
        Task<HttpResponseMessage> UpdateEducationAsync(UpdateEducationDetailCommand command);
        Task<HttpResponseMessage> DeleteEducationAsync(Guid id);

        Task<bool> SendConfirmationCode(string phoneNumber);
  
    }

}
