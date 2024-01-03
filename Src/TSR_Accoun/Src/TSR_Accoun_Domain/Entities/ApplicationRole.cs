using Microsoft.AspNetCore.Identity;

namespace TSR_Accoun_Domain.Entities
{
	public class ApplicationRole : IdentityRole<Guid>
	{
		public string Slug { get; set; }
	}
}
