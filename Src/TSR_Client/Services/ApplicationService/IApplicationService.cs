using Application.Contracts.Applications.Commands.CreateApplication;
using Application.Contracts.Applications.Responses;
using Application.Contracts.Common;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts.Applications.Commands.UpdateApplicationStatus;
using static Application.Contracts.Dtos.Enums.ApplicationStatusDto;

namespace TSR_Client.Services.ApplicationService
{
    public interface IApplicationService
    {
        Task<List<ApplicationListStatus>> GetApplicationsByStatus(ApplicationStatus status);
        Task CreateApplication(CreateApplicationCommand application);
        Task<PagedList<ApplicationListDto>> GetAllApplications();
        Task<bool> UpdateStatus(UpdateApplicationStatuss updateApplicationStatus);
        Task<ApplicationDetailsDto> GetApplicationDetails(string applicationSlug);
        Task<string> GetCvLinkAsync(string slug);
    }
}
