using Microsoft.AspNetCore.Identity;
using TSR_Accoun_Domain.Enumes;

namespace TSR_Accoun_Domain.Entities
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		public Gender Gender { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string AboutMyself { get; set; }
		public List<EducationDetail> Educations { get; set; }

	}
}
