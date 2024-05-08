using Application.Contracts.Applications.Responses;
using static Application.Contracts.Dtos.Enums.ApplicationStatusDto;

namespace Application.Contracts.Applications.Queries.GetApplicationsByStatus;

public class GetApplicationsByStatusQuery : IRequest<List<ApplicationListStatus>>
{
    public ApplicationStatus Status { get; set; }
}
