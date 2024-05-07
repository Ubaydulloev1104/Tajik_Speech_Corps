using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net.Http;
using TSR_Accoun_Application.Contracts.User.Commands.ChangePassword;
using TSR_Accoun_Application.Contracts.User.Commands.LoginUser;
using TSR_Accoun_Application.Contracts.User.Commands.RegisterUser;
using System.Net;
using System;
using TSR_Accoun_Application.Contracts.User.Responses;
using TSR_Client.Identity;
using TSR_Accoun_Application.Contracts.User.Queries.CheckUserDetails;
using AltairCA.Blazor.WebAssembly.Cookie;
using TSR_Client.Services.Profile;

namespace TSR_Client.Services.Auth
{
    public class AuthService(IdentityHttpClient identityHttpClient,
         AuthenticationStateProvider authenticationStateProvider, NavigationManager navigationManager,
         IAltairCABlazorCookieUtil cookieUtil, LayoutService layoutService, IUserProfileService userProfileService)
     : IAuthService
    {
        public async Task<HttpResponseMessage> ChangePassword(ChangePasswordUserCommand command)
        {

            var result = await identityHttpClient.PutAsJsonAsync("Auth/ChangePassword", command);
            return result;
        }

        public async Task<string> LoginUserAsync(LoginUserCommand command, bool newRegister = false)
        {
            string errorMessage = null;
            try
            {
                var result = await identityHttpClient.PostAsJsonAsync("Auth/login", command);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<JwtTokenResponse>();
                    await cookieUtil.SetValueAsync("authToken", response);
                    await authenticationStateProvider.GetAuthenticationStateAsync();
                    layoutService.User = await userProfileService.Get();
                    if (!newRegister)
                        navigationManager.NavigateTo("/");
                    return null;
                }

                if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    errorMessage = (await result.Content.ReadFromJsonAsync<CustomProblemDetails>()).Detail;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
                errorMessage = "Server is not responding, please try later";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorMessage = "An error occurred";
            }

            return errorMessage;
        }

        public async Task<string> RegisterUserAsync(RegisterUserCommand command)
        {
            try
            {
                command.PhoneNumber = command.PhoneNumber.Trim();
                if (command.PhoneNumber.Length == 9) command.PhoneNumber = "+992" + command.PhoneNumber.Trim();
                else if (command.PhoneNumber.Length == 12 && command.PhoneNumber[0] != '+') command.PhoneNumber = "+" + command.PhoneNumber;

                var result = await identityHttpClient.PostAsJsonAsync("Auth/register", command);
                if (result.IsSuccessStatusCode)
                {
                    await LoginUserAsync(new LoginUserCommand()
                    {
                        Password = command.Password,
                        Username = command.Username
                    });
                    layoutService.User = await userProfileService.Get();
                    navigationManager.NavigateTo("/profile");

                    return "";
                }
                if (result.StatusCode is not (HttpStatusCode.Unauthorized or HttpStatusCode.BadRequest))
                    return "server error, please try again later";

                var response = await result.Content.ReadFromJsonAsync<CustomProblemDetails>();
                return response.Detail;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
                return "Server is not responding, please try later";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "An error occurred";
            }
        }

        public async Task<HttpResponseMessage> CheckUserName(string userName)
        {
            var result = await identityHttpClient.GetAsync($"User/CheckUserName/{userName}");
            return result;
        }

        public async Task<HttpResponseMessage> CheckUserDetails(CheckUserDetailsQuery query)
        {
            var result = await identityHttpClient.GetAsync($"User/CheckUserDetails/{query.UserName}/{query.PhoneNumber}/{query.Email}");
            return result;
        }

       
    }

}
