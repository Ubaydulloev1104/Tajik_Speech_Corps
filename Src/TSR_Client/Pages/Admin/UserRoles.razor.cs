using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TSR_Accoun_Application.Contracts.Profile.Responses;
using TSR_Accoun_Application.Contracts.UserRoles.Commands;
using TSR_Accoun_Application.Contracts.UserRoles.Response;
using TSR_Client.Services.Profile;

namespace TSR_Client.Pages.Admin
{
    public partial class UserRoles
    {
        [Parameter] public string Username { get; set; }
        private List<UserRolesResponse> Roles { get; set; }
        [Inject] private IdentityHttpClient HttpClient { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private IUserProfileService UserProfileService { get; set; }
        private string NewRoleName { get; set; }

        private bool _isOpened;

        private UserProfileResponse personalData = new UserProfileResponse();
       
        protected override async Task OnInitializedAsync()
        {
            await ReloadDataAsync();
            personalData = await UserProfileService.Get(Username);
        }

        private async Task ReloadDataAsync()
        {
            await AuthStateProvider.GetAuthenticationStateAsync();
            var userRolesResponse = await HttpClient.GetAsync($"UserRoles?userName={Username}");
            if (!userRolesResponse.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/notfound");
                return;
            }

            Roles = await userRolesResponse.Content.ReadFromJsonAsync<List<UserRolesResponse>>();
        }

        private async Task OnDeleteClick(string contextSlug)
        {
            if (!string.IsNullOrWhiteSpace(contextSlug))
            {
                await AuthStateProvider.GetAuthenticationStateAsync();
                try
                {
                    var deleteResult = await HttpClient.DeleteAsync($"UserRoles/{contextSlug}");
                    if (!deleteResult.IsSuccessStatusCode)
                    {
                        Snackbar.Add("Deleting error", Severity.Error);
                        return;
                    }
                }
                catch (Exception)
                {
                    Snackbar.Add("Server is not responding, please try later", Severity.Error);
                    return;
                }

                await ReloadDataAsync();
                StateHasChanged();
            }
        }

        private async Task OnAddClick()
        {
            if (!string.IsNullOrWhiteSpace(NewRoleName))
            {
                var userRoleCommand = new CreateUserRolesCommand { RoleName = NewRoleName, UserName = Username };

                await AuthStateProvider.GetAuthenticationStateAsync();
                try
                {
                    var userRoleResponse = await HttpClient.PostAsJsonAsync("UserRoles", userRoleCommand);
                    if (!userRoleResponse.IsSuccessStatusCode)
                    {
                        Snackbar.Add("Add error may be you entered duplicate role");
                        return;
                    }
                }
                catch (Exception)
                {
                    Snackbar.Add("Server is not responding, please try later", Severity.Error);
                    return;
                }

                await ReloadDataAsync();
                StateHasChanged();
            }
        }
    }
}

