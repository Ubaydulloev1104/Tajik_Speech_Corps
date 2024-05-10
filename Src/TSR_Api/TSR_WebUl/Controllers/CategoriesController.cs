using Application.Contracts.Common;
using Application.Contracts.Word.Queries.GetWordCategories;
using Application.Contracts.WordCategore.Commands.CreateWordCategory;
using Application.Contracts.WordCategore.Commands.DeleteWordCategory;
using Application.Contracts.WordCategore.Commands.UpdateWordCategory;
using Application.Contracts.WordCategore.Queries.GetWordCategoryWithPagination;
using Application.Contracts.WordCategore.Responses;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TSR_WebUl.Controllers;

[Route("api/[controller]")]
[ApiController]
[Application.Common.Security.Authorize]
public class CategoriesController : ApiControllerBase
{
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ILogger<CategoriesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<CategoryResponse>>> GetAll(
        [FromQuery] PagedListQuery<CategoryResponse> query)
    {
        var categories = await Mediator.Send(query);
        return Ok(categories);
    }

    [HttpGet("Word")]
    public async Task<IActionResult> GetCategories([FromQuery] GetWordCategoriesQuery query)
    {
        var categories = await Mediator.Send(query);
        return Ok(categories);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> Get(string slug)
    {
        var category = await Mediator.Send(new GetWordCategoryBySlugQuery { Slug = slug });
        return Ok(category);
    }

    [HttpPost]
    [Authorize(ApplicationPolicies.Reviewer)]
    public async Task<ActionResult<string>> CreateNewCategoryVacancy(CreateWordCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { slug = result }, result);
    }


    [HttpPut("{slug}")]
    [Authorize(ApplicationPolicies.Reviewer)]
    public async Task<ActionResult<string>> Update([FromRoute] string slug,
        [FromBody] UpdateWordCategoryCommand request, CancellationToken cancellationToken)
    {
        if (slug != request.Slug)
            return BadRequest();

        var result = await Mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { slug = result }, result);
    }

    [HttpDelete("{slug}")]
    [Authorize(ApplicationPolicies.Reviewer)]
    public async Task<IActionResult> Delete(string slug)
    {
        var command = new DeleteWordCategoryCommand { Slug = slug };
        await Mediator.Send(command);
        return NoContent();
    }
}
