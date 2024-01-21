using MediatR;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Application.Common.Exceptions;
using TSR_Accoun_Application.Common.Interfaces.DbContexts;
using TSR_Accoun_Application.Common.Interfaces.Services;
using TSR_Accoun_Application.Contracts.Educations.Command.Create;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.Educations.Commands
{
	public class
	CreateEducationDetailCommandHandler : IRequestHandler<CreateEducationDetailCommand, Guid>
	{
		private readonly IApplicationDbContext _context;
		private readonly IUserHttpContextAccessor _userHttpContextAccessor;

		public CreateEducationDetailCommandHandler(IApplicationDbContext context,
			IUserHttpContextAccessor userHttpContextAccessor)
		{
			_context = context;
			_userHttpContextAccessor = userHttpContextAccessor;
		}
		public async Task<Guid> Handle(CreateEducationDetailCommand request, CancellationToken cancellationToken)
		{
			var userId = _userHttpContextAccessor.GetUserId();
			var user = await _context.Users
				.Include(u => u.Educations)
				.FirstOrDefaultAsync(u => u.Id.Equals(userId));
			_ = user ?? throw new NotFoundException("user is not found");

			var textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
			var university = textInfo.ToTitleCase(request.University.Trim());
			var speciality = textInfo.ToTitleCase(request.Speciality.Trim());

			var existingEducation = user.Educations.FirstOrDefault(e =>
				e.University.Equals(university, StringComparison.OrdinalIgnoreCase) &&
				e.Speciality.Equals(speciality, StringComparison.OrdinalIgnoreCase));

			if (existingEducation != null)
			{
				throw new DuplicateWaitObjectException("Education detail already exists.");
			}

			var education = new EducationDetail()
			{
				University = university,
				Speciality = speciality,
				StartDate = request.StartDate.HasValue ? request.StartDate.Value : default(DateTime),
				EndDate = request.EndDate.HasValue ? request.EndDate.Value : default(DateTime),
				UntilNow = request.UntilNow
			};

			user.Educations.Add(education);
			await _context.SaveChangesAsync();
			return education.Id;
		}

	}
}
