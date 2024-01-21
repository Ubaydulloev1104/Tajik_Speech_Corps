using Microsoft.AspNetCore.Identity;

namespace TSR_Accoun_Domain.Entities
{
	public class ApplicationUserClaim :IdentityUserClaim<Guid>
	{
		public string Slug { get; set; }
	}
}

