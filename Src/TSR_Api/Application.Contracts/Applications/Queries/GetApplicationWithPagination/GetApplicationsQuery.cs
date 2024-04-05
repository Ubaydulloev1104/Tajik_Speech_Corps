using Application.Contracts.Applications.Responses;

namespace Application.Contracts.Applications.Queries.GetApplicationWithPagination
{
    public class GetApplicationsQuery : IRequest<List<ApplicationListDto>>
    {
    }
}
