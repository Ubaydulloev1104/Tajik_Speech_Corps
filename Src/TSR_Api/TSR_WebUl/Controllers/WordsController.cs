using Application.Contracts.Common;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.JavaScript;
using Application.Contracts.Word.Responses;
using Application.Contracts.Word.Queries.GetWordBySlug;
using Application.Contracts.Word.Commands.Create;
using Application.Contracts.Word.Commands.Update;
using Application.Contracts.Word.Commands.Delete;

namespace TSR_WebUl.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize(ApplicationPolicies.Reviewer)]
public class WordsController : ApiControllerBase
{
    private readonly ILogger<WordsController> _logger;

    public WordsController(ILogger<WordsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] PagedListQuery<WordListDto> query)
    {
        var categories = await Mediator.Send(query);
        return Ok(categories);
    }

    [HttpGet("{slug}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetWordBySlug([FromRoute] string slug)
    {
        var category = await Mediator.Send(new GetWordBySlugQuery { Slug = slug });
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<JSObject>> CreateNewJobVacancy([FromBody] CreateWordCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetWordBySlug), new { slug = result }, result);
    }

    [HttpPut("{slug}")]
    public async Task<ActionResult<string>> Update([FromRoute] string slug, [FromBody] UpdateWordCommand request, CancellationToken cancellationToken)
    {
        if (slug != request.Slug)
            return BadRequest();

        var result = await Mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetWordBySlug), new { slug = result }, result);
    }

    [HttpDelete("{slug}")]
    public async Task<ActionResult<bool>> DeleteJobVacancy([FromRoute] string slug, CancellationToken cancellationToken)
    {
        var request = new DeleteWordCommand { Slug = slug };
        return await Mediator.Send(request, cancellationToken);
    }
}
