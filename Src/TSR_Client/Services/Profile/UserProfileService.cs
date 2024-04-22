using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using TSR_Accoun_Application.Contracts.Educations.Command.Create;
using TSR_Accoun_Application.Contracts.Educations.Command.Update;
using TSR_Accoun_Application.Contracts.Educations.Responsess;
using TSR_Accoun_Application.Contracts.Profile.Commands.UpdateProfile;
using TSR_Accoun_Application.Contracts.Profile.Responses;

namespace TSR_Client.Services.Profile
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IdentityHttpClient _identityHttpClient;

        public UserProfileService(IdentityHttpClient identityHttpClient)
        {
            _identityHttpClient = identityHttpClient;
        }
        public async Task<string> Update(UpdateProfileCommand command)
        {
            try
            {
                var result = await _identityHttpClient.PutAsJsonAsync("Profile", command);

                if (result.IsSuccessStatusCode)
                    return "";

                if (result.StatusCode == HttpStatusCode.BadRequest)
                    return result.RequestMessage.ToString();

                return "Server is not responding, please try later";

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return "Server is not responding, please try later";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "An error occurred";
            }
        }

        public async Task<UserProfileResponse> Get(string userName = null)
        {
            var result = await _identityHttpClient
              .GetFromJsonAsync<UserProfileResponse>($"Profile{(userName != null ? "?userName=" + Uri.EscapeDataString(userName) : "")}");
            return result;
        }

        public async Task<List<UserEducationResponse>> GetEducationsByUser()
        {
            var result = await _identityHttpClient.GetFromJsonAsync<List<UserEducationResponse>>("Profile/GetEducationsByUser");
            return result;
        }

        public async Task<HttpResponseMessage> CreateEducationAsуnc(CreateEducationDetailCommand command)
        {
            var response = await _identityHttpClient.PostAsJsonAsync("Profile/CreateEducationDetail", command);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateEducationAsync(UpdateEducationDetailCommand command)
        {
            var response = await _identityHttpClient.PutAsJsonAsync("Profile/UpdateEducationDetail", command);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteEducationAsync(Guid id)
        {
            var respose = await _identityHttpClient.DeleteAsync($"Profile/DeleteEducationDetail/{id}");
            return respose;
        }

 

        public async Task<bool> SendConfirmationCode(string phoneNumber)
        {
            var response = await _identityHttpClient.GetFromJsonAsync<bool>($"sms/send_code?PhoneNumber={phoneNumber}");

            return response;
        }

        public async Task<List<UserEducationResponse>> GetAllEducations()
        {
            var result = await _identityHttpClient.GetFromJsonAsync<List<UserEducationResponse>>("Profile/GetAllEducations");
            return result;
        }

    }
}
