using System.Net.Http;
using System.Threading.Tasks;
using TSR_Accoun_Application.Contracts.User.Commands.ChangePassword;
using TSR_Accoun_Application.Contracts.User.Commands.LoginUser;
using TSR_Accoun_Application.Contracts.User.Commands.RegisterUser;

namespace TSR_Client.Services.Auth
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(RegisterUserCommand command);
        Task<string> LoginUserAsync(LoginUserCommand command, bool newRegister = false);
        Task<HttpResponseMessage> ChangePassword(ChangePasswordUserCommand command);
        Task<HttpResponseMessage> CheckUserName(string userName);
    }

}
