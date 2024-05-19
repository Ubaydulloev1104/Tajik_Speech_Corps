using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using MudBlazor;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Contracts.Applications.Responses;
using System.Net;
using System.Collections.Generic;
using static Application.Contracts.Dtos.Enums.ApplicationStatusDto;
using Application.Contracts.Applications.Commands.CreateApplication;
using Application.Contracts.Common;
using Application.Contracts.Applications.Commands.UpdateApplicationStatus;
using System.Linq;
using TSR_Client.Identity;
using System;
using Microsoft.AspNetCore.Components.Forms;
using System.IO;

namespace TSR_Client.Services.ApplicationService
{
    public class ApplicationService(
     HttpClient httpClient,
     AuthenticationStateProvider authenticationState,
     ISnackbar snackbar,
     NavigationManager navigationManager,
     IConfiguration configuration)
     : IApplicationService
    {
        private const string ApplicationsEndPoint = "applications";

        public async Task<List<ApplicationListStatus>> GetApplicationsByStatus(ApplicationStatus status)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{ApplicationsEndPoint}/{status}");

            List<ApplicationListStatus> result = await response.Content.ReadFromJsonAsync<List<ApplicationListStatus>>();
            return result;
        }


		public async Task CreateApplication(CreateApplicationCommand application)
		{
			try
			{
				var response = await httpClient.PostAsJsonAsync(ApplicationsEndPoint, application);
				switch (response.StatusCode)
				{
					case HttpStatusCode.OK:
						snackbar.Add("Applications sent successfully!", Severity.Success);
						navigationManager.NavigateTo(navigationManager.Uri.Replace("/apply/", "/"));
						break;
					case HttpStatusCode.Conflict:
						snackbar.Add((await response.Content.ReadFromJsonAsync<CustomProblemDetails>()).Detail,
									   Severity.Error);
						break;
					case HttpStatusCode.BadRequest:
						snackbar.Add("Invalid application data. Please check your information.", Severity.Error);
						break;
					case HttpStatusCode.InternalServerError:
						snackbar.Add("Server error. Please try again later.", Severity.Error);
						
						break;
					default:
						snackbar.Add($"Unknown error: {response.StatusCode}", Severity.Error);
						break;
				}
			}
			catch (Exception e)
			{
				snackbar.Add("Server is not responding, please try later", Severity.Error);
				Console.WriteLine(e.Message);
			}
		}

		private async Task<byte[]> GetFileBytesAsync(IBrowserFile file)
        {
            if (file.Size <= int.Parse(configuration["CvSettings:MaxFileSize"]!) * 1024 * 1024)
            {
                var ms = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(ms);
                var res = ms.ToArray();
                return res;
            }

            return null;
        }

        public async Task<PagedList<ApplicationListDto>> GetAllApplications()
        {
            await authenticationState.GetAuthenticationStateAsync();
            HttpResponseMessage response = await httpClient.GetAsync(ApplicationsEndPoint);
            PagedList<ApplicationListDto>
                result = await response.Content.ReadFromJsonAsync<PagedList<ApplicationListDto>>();
            return result;
        }

        public async Task<bool> UpdateStatus(UpdateApplicationStatuss updateApplicationStatus)
        {
            await authenticationState.GetAuthenticationStateAsync();
            HttpResponseMessage response =
                await httpClient.PutAsJsonAsync($"{ApplicationsEndPoint}/{updateApplicationStatus.Slug}/update-status",
                    updateApplicationStatus);
            bool result = await response.Content.ReadFromJsonAsync<bool>();
            return result;
        }

        public async Task<ApplicationDetailsDto> GetApplicationDetails(string applicationSlug)
        {
            await authenticationState.GetAuthenticationStateAsync();
            HttpResponseMessage response = await httpClient.GetAsync($"{ApplicationsEndPoint}/{applicationSlug}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var application = await response.Content.ReadFromJsonAsync<ApplicationDetailsDto>();
                return application;
            }

            return null;
        }

        public async Task<string> GetCvLinkAsync(string slug)
        {
            return "";
        }

        private async Task<string> GetCurrentUserName()
        {
            var authState = await authenticationState.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                var userNameClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Username);
                if (userNameClaim != null)
                    return userNameClaim.Value;
            }

            return null;
        }
    }
}
