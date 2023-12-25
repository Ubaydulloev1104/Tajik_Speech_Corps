using MediatR;
using TSR_Accoun_Application.Contracts.Profile.Responses;

namespace TSR_Accoun_Application.Contracts.Profile.Queries
{
	public class GetPofileQuery : IRequest<UserProfileResponse>
	{
		public string UserName { get; set; }
	}
}
