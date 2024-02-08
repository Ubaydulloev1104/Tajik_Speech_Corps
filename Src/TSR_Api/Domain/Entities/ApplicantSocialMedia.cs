using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
	public class ApplicantSocialMedia : BaseEntity
	{
		public string ProfileUrl { get; set; }
		public SocialMediaType Type { get; set; }

		public Guid UserId { get; set; }
	}
}
