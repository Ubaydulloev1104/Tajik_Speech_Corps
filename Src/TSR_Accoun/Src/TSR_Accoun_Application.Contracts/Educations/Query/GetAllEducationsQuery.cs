using MediatR;
using TSR_Accoun_Application.Contracts.Educations.Responsess;

namespace TSR_Accoun_Application.Contracts.Educations.Query
{
	public class GetAllEducationsQuery : IRequest<List<UserEducationResponse>>
	{
	}
}
