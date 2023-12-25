using MediatR;
using TSR_Accoun_Application.Contracts.User.Responses;

namespace TSR_Accoun_Application.Contracts.User.Queries.CheckUserDetails
{
	public class CheckUserDetailsQuery : IRequest<UserDetailsResponse>
	{
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
	}
}
