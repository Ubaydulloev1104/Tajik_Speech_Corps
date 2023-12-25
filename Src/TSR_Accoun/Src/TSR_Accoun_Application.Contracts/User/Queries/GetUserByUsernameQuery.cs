using MediatR;
using TSR_Accoun_Application.Contracts.User.Responses;

namespace TSR_Accoun_Application.Contracts.User.Queries
{
	public class GetUserByUsernameQuery : IRequest<UserResponse>
	{
		public string UserName { get; set; }
	}
}
