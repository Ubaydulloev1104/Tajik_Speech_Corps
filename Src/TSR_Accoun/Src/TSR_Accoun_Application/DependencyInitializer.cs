using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TSR_Accoun_Application.Common.Interfaces.Services;
using TSR_Accoun_Application.Features.Users;
using TSR_Accoun_Application.Services;

namespace TSR_Accoun_Application
{
	public static class DependencyInitializer
	{
		public static void AddApplication(this IServiceCollection services)
		{
			services.AddOptions();
			services.AddMediatR(s => s.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			services.AddAutoMapper(typeof(DependencyInitializer).Assembly);
			services.AddAutoMapper(typeof(UsersProfile).Assembly);
			Assembly assem = Assembly.GetExecutingAssembly();
			services.AddValidatorsFromAssembly(assem);
			services.AddScoped<IJwtTokenService, JwtTokenService>();
		}
	}
}
