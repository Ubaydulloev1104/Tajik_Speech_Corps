using Application.Contracts.Applications.Responses;

namespace Application.Contracts.Applications.Queries.GetApplicationBySlug;

public class GetBySlugApplicationQuery : IRequest<ApplicationDetailsDto>
{
    public string Slug { get; set; }
}
