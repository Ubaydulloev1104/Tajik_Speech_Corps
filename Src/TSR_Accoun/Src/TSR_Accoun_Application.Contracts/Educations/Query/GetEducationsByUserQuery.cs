using MediatR;
using TSR_Accoun_Application.Contracts.Educations.Responsess;

namespace TSR_Accoun_Application.Contracts.Educations.Query
{
	public class GetEducationsByUserQuery : IRequest<List<UserEducationResponse>>
	{
		public string UserName { get; set; }
	}
}
