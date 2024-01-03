using AutoMapper;
using TSR_Accoun_Application.Contracts.User.Responses;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.Users
{
	public class UsersProfile : Profile
	{
		public UsersProfile()
		{
			CreateMap<ApplicationUser, UserResponse>()
				.ForMember(dest => dest.FullName, opt => opt
				.MapFrom(src => $"{src.FirstName} {src.LastName}"));
		}
	}
}
