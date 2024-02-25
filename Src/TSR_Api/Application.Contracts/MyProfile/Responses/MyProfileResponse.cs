using Application.Contracts.Converter.Converter;
using System.Text.Json.Serialization;

namespace Application.Contracts.MyProfile.Responses
{
	public class MyProfileResponse
	{
		public string Avatar { get; set; }

		[JsonConverter(typeof(DateTimeToUnixConverter))]
		public DateTime DateOfBirth { get; set; }

		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string Patronymic { get; set; }
		public Dtos.Enums.ApplicationStatusDto.Gender Gender { get; set; }
		public MyIdentityResponse Identity { get; set; }
	}
}
