using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TSR_Accoun_Application.Common.Interfaces.DbContexts;
using TSR_Accoun_Application.Common.Interfaces.Services;
using TSR_Accoun_Domain.Entities;
using TSR_Accoun_Infrastructure.Account.Services;
using TSR_Accoun_Infrastructure.Identity;
using TSR_Accoun_Infrastructure.Persistence;

namespace TSR_Accoun_Infrastructure
{
	public static class DependencyInitializer
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configurations)
		{

			services.AddDbContext<ApplicationDbContext>(options =>
			{
				string dbConnectionString = configurations.GetConnectionString("DefaultConnection");
				if (configurations["UseInMemoryDatabase"] == "true")
					options.UseInMemoryDatabase("testDB");
				else
					options.UseSqlServer(dbConnectionString);
			});

			services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
			services.AddScoped<ApplicationDbContextInitializer>();

			services.AddScoped<DbMigration>();
			services.AddHttpClient();

			services.AddScoped<IUserHttpContextAccessor, UserHttpContextAccessor>();

			services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
			{
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 8;
				options.ClaimsIdentity.EmailClaimType = ClaimTypes.Email;
				options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
				options.ClaimsIdentity.UserIdClaimType = ClaimTypes.Id;
				options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Username;
				options.User.RequireUniqueEmail = false;
			})
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();



			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			services.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(op =>
			{
				op.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey =
						new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(configurations["JWT:Secret"])),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			services.AddAuthorization(auth =>
			{
				auth.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
					.RequireAuthenticatedUser()
					.Build();

				auth.AddPolicy(ApplicationPolicies.SuperAdministrator, op => op
					.RequireRole(ApplicationClaimValues.SuperAdministrator));

				auth.AddPolicy(ApplicationPolicies.Administrator, op => op
					.RequireRole(ApplicationClaimValues.SuperAdministrator, ApplicationClaimValues.Administrator));
			});

			var corsAllowedHosts = configurations.GetSection("MraIdentity-CORS").Get<string[]>();
			services.AddCors(options =>
			{
				options.AddPolicy("CORS_POLICY", policyConfig =>
				{
					policyConfig.WithOrigins(corsAllowedHosts)
								.AllowAnyHeader()
								.AllowAnyMethod();
				});
			});
		}


	}
}
