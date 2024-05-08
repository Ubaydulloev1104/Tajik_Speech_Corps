using Application.Contracts.Applications.Responses;
using Application.Contracts.Common;

namespace Application.Contracts.Applications.Queries.GetApplicationWithPagination
{
    public class GetApplicationsByFiltersQuery : PagedListQuery<ApplicationListDto>
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
    }
}
