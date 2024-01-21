using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Application.Common.Exceptions;
using TSR_Accoun_Application.Common.Interfaces.DbContexts;
using TSR_Accoun_Application.Common.Interfaces.Services;
using TSR_Accoun_Application.Contracts.Educations.Command.Update;

namespace TSR_Accoun_Application.Features.Educations.Commands
{
	public class
	UpdateEducationDetailCommandHandler : IRequestHandler<UpdateEducationDetailCommand, Guid>
	{
		private readonly IApplicationDbContext _context;
		private readonly IUserHttpContextAccessor _userHttpContextAccessor;
		private readonly IMapper _mapper;

		public UpdateEducationDetailCommandHandler(IApplicationDbContext context,
			IUserHttpContextAccessor userHttpContextAccessor,
			IMapper mapper)
		{
			_context = context;
			_userHttpContextAccessor = userHttpContextAccessor;
			_mapper = mapper;
		}

		public async Task<Guid> Handle(UpdateEducationDetailCommand request, CancellationToken cancellationToken)
		{
			var userId = _userHttpContextAccessor.GetUserId();
			var user = await _context.Users
				.Include(u => u.Educations)
				.FirstOrDefaultAsync(u => u.Id.Equals(userId));
			_ = user ?? throw new NotFoundException("user is not found");

			var education = user.Educations.FirstOrDefault(e => e.Id.Equals(request.Id));
			_ = education ?? throw new NotFoundException("education not exits");

			var textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
			var university = textInfo.ToTitleCase(request.University.Trim());
			var speciality = textInfo.ToTitleCase(request.Speciality.Trim());

			var existingEducation = user.Educations.FirstOrDefault(e =>
				e.Id != request.Id &&
				e.University.Equals(university, StringComparison.OrdinalIgnoreCase) &&
				e.Speciality.Equals(speciality, StringComparison.OrdinalIgnoreCase));

			if (existingEducation != null)
			{
				throw new DuplicateWaitObjectException("Education detail already exists.");
			}

			education.University = university;
			education.Speciality = speciality;
			education.StartDate = request.StartDate.HasValue ? request.StartDate.Value : default(DateTime);
			education.EndDate = request.EndDate.HasValue ? request.EndDate.Value : default(DateTime);
			education.UntilNow = request.UntilNow;

			await _context.SaveChangesAsync();
			return education.Id;
		}
	}
}
