using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TSR_Accoun_Application
{
	public static class DependencyInitializer
	{
		public static void AddApplication(this IServiceCollection services)
		{
			services.AddOptions();
			services.AddMediatR(s => s.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			services.AddAutoMapper(typeof(DependencyInitializer).Assembly);
		}
	}
}
