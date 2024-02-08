using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
	public class Application : BaseAuditableEntity
	{
		public string CoverLetter { get; set; }
		public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
		public ApplicationStatus Status { get; set; }
		public string ApplicantUsername { get; set; }
		public Guid ApplicantId { get; set; }

		public ICollection<ApplicationTimelineEvent> History { get; set; }
		public string Slug { get; set; }

	}
}
