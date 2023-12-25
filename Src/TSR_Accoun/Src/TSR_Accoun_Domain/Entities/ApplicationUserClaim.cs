using Microsoft.AspNetCore.Identity;

namespace TSR_Accoun_Domain.Entities
{
	public class ApplicationUserClaim : IdentityRole<Guid>
	{
		public string Slug { get; set; }
		public object ClaimValue { get; set; }
	}
}

