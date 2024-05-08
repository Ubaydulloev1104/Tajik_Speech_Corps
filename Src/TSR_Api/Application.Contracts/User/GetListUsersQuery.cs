using TSR_Accoun_Application.Contracts.User.Responses;

namespace Application.Contracts.User;

public class GetListUsersQuery : IRequest<List<UserResponse>>
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Skills { get; set; }
}
