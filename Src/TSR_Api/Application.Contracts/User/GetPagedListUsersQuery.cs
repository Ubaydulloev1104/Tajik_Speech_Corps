using Application.Contracts.Common;
using TSR_Accoun_Application.Contracts.User.Responses;

namespace Application.Contracts.User;

public class GetPagedListUsersQuery : IRequest<PagedList<UserResponse>>
{
    public string Filters { get; set; }
    public string Sorts { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string Skills { get; set; }
}
