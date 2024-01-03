using AutoMapper;
using TSR_Accoun_Application.Contracts.Educations.Command.Update;
using TSR_Accoun_Application.Contracts.Educations.Responsess;
using TSR_Accoun_Domain.Entities;

namespace TSR_Accoun_Application.Features.Educations
{
	public class EducationDetailProfile : Profile
	{
		public EducationDetailProfile()
		{
			CreateMap<UpdateEducationDetailCommand, EducationDetail>();
			CreateMap<EducationDetail, UserEducationResponse>();
		}
	}
}
