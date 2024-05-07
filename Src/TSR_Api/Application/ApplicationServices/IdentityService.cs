using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TSR_Accoun_Application.Contracts.Profile.Responses;

namespace Application.ApplicationServices
{
    public class IdentityService : IidentityService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _factory;

        public IdentityService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IHttpClientFactory factory)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _factory = factory;
        }

        public async Task<UserProfileResponse> ApplicantDetailsInfo(string userName = null)
        {
            using var identityHttpClient = _factory.CreateClient("IdentityHttpClientProfile");
            var applicantDetails = new UserProfileResponse();

            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                var token = authorizationHeader.Split(' ').Last();
                identityHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            using var response = await identityHttpClient.GetAsync(
                $"{_configuration["MraJobs-IdentityApi:Profile"]}{(userName != null ? $"?userName={userName}" : "")}");
            if (response.IsSuccessStatusCode)
            {
                applicantDetails = await response.Content.ReadFromJsonAsync<UserProfileResponse>();
            }

            return applicantDetails;
        }
    }

}
