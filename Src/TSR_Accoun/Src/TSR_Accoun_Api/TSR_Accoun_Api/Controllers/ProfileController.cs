using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSR_Accoun_Application.Contracts.Educations.Command.Create;
using TSR_Accoun_Application.Contracts.Educations.Command.Delete;
using TSR_Accoun_Application.Contracts.Educations.Command.Update;
using TSR_Accoun_Application.Contracts.Educations.Query;
using TSR_Accoun_Application.Contracts.Profile.Commands.UpdateProfile;
using TSR_Accoun_Application.Contracts.Profile.Queries;

namespace TSR_Accoun_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]

	public class ProfileController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProfileController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPut]
		public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetProfileByUserName([FromQuery] string userName = null)
		{
			var result = await _mediator.Send(new GetPofileQuery { UserName = userName });
			return Ok(result);
		}
		[HttpPost("CreateEducationDetail")]
		public async Task<IActionResult> CreateEducationDetail([FromBody] CreateEducationDetailCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete("DeleteEducationDetail/{id}")]
		public async Task<IActionResult> DeleteEducationDetail([FromRoute] Guid id)
		{
			var result = await _mediator.Send(new DeleteEducationCommand { Id = id });
			return Ok(result);
		}

		[HttpPut("UpdateEducationDetail")]
		public async Task<IActionResult> UpdateEducationDetail([FromBody] UpdateEducationDetailCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpGet("GetEducationsByUser")]
		public async Task<IActionResult> GetEducationsByUser([FromQuery] GetEducationsByUserQuery query)
		{
			var result = await _mediator.Send(query);

			return Ok(result);
		}

		[HttpGet("GetAllEducations")]
		public async Task<IActionResult> GetAllEducations()
		{
			var result = await _mediator.Send(new GetAllEducationsQuery());
			return Ok(result);
		}
	}
}
