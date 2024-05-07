using Application.Contracts.Applications.Commands.AddNote;
using Application.Contracts.Applications.Commands.CreateApplication;
using Application.Contracts.Applications.Commands.Delete;
using Application.Contracts.Applications.Commands.UpdateApplication;
using Application.Contracts.Applications.Commands.UpdateApplicationStatus;
using Application.Contracts.Applications.Queries.GetApplicationBySlug;
using Application.Contracts.Applications.Responses;
using Application.Contracts.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TSR_WebUl.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ApplicationsController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<PagedList<ApplicationListDto>>> GetAll(
        [FromQuery] PagedListQuery<ApplicationListDto> query)
    {
        var applications = await Mediator.Send(query);
        return Ok(applications);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<ApplicationDetailsDto>> Get(string slug, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetBySlugApplicationQuery { Slug = slug }, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateApplication(CreateApplicationCommand request,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }


    [HttpPut("{slug}")]
    public async Task<ActionResult<Guid>> UpdateApplication(string slug, UpdateApplicationCommand request,
        CancellationToken cancellationToken)
    {
        request.Slug = slug;
        return await Mediator.Send(request, cancellationToken);
    }

    [HttpDelete("{slug}")]
    public async Task<ActionResult<bool>> DeleteApplication(string slug, CancellationToken cancellationToken)
    {
        var request = new DeleteApplicationCommand { Slug = slug };
        return await Mediator.Send(request, cancellationToken);
    }

    [HttpPut("{slug}/update-status")]
    public async Task<ActionResult<bool>> UpdateStatus(string slug, UpdateApplicationStatuss request,
        CancellationToken cancellationToken)
    {
        request.Slug = slug;
        return await Mediator.Send(request, cancellationToken);
    }

    [HttpPost("{slug}/add-note")]
    public async Task<ActionResult<bool>> AddNote(string slug, AddNoteToApplicationCommand request,
        CancellationToken cancellationToken)
    {
        request.Slug = slug;
        return await Mediator.Send(request, cancellationToken);
    }
}
