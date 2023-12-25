using MediatR;

namespace TSR_Accoun_Application.Contracts.Educations.Command.Create
{
	public class CreateEducationDetailCommand : IRequest<Guid>
	{
		public string University { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool UntilNow { get; set; }
		public string Speciality { get; set; }
	}
}
