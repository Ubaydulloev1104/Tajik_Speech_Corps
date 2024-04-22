namespace Application.Common.Interfaces
{
    public interface IUserHttpContextAccessor
    {
        bool IsAuthenticated();
        Guid GetUserId();
        String GetUserName();

        List<string> GetUserRoles();
    }
}
