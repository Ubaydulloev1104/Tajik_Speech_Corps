using AutoMapper;
using TSR_Accoun_Application.Contracts.Profile.Commands.UpdateProfile;
using TSR_Accoun_Application.Contracts.Profile.Responses;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.UserProfiles
{
	public class UserProfilesProfile : Profile
	{
		public UserProfilesProfile()
		{
			CreateMap<UpdateProfileCommand, ApplicationUser>();
			CreateMap<ApplicationUser, UserProfileResponse>();
		}
	}
}
