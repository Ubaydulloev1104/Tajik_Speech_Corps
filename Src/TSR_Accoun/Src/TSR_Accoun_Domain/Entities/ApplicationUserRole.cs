using Microsoft.AspNetCore.Identity;

namespace TSR_Accoun_Domain.Entities
{
	public class ApplicationUserRole : IdentityUserRole<Guid>
	{
		public string Slug { get; set; }
	}
}
