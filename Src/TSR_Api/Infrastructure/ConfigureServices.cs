using Infrastructure.Identity.Services;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Identity;
using Application;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAppIdentity(configuration);
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();


        services.AddDbContext<ApplicationDbContext>(options =>
        {
            string dbConnectionString = configuration.GetConnectionString("DefaultConnection");
            if (configuration["UseInMemoryDatabase"] == "true")
                options.UseInMemoryDatabase("testDb");
            else
                options.UseSqlServer(dbConnectionString);
        });
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<DbMigration>();
        services.AddScoped<ApplicationDbContextInitializer>();
        services.AddScoped<IUserHttpContextAccessor, UserHttpContextAccessor>();
        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }

    private static IServiceCollection AddAppIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();

        services.AddScoped<ICurrentUserService, CurrentUserService>();


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
                        System.Text.Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddAuthorization(auth =>
        {
            auth.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .RequireRole(ApplicationClaimValues.Applicant, ApplicationClaimValues.Reviewer,
                    ApplicationClaimValues.SuperAdmin,
                    ApplicationClaimValues.Administrator)
                .RequireClaim(ClaimTypes.Id)
                .RequireClaim(ClaimTypes.Application, ApplicationClaimValues.ApplicationName,
                    ApplicationClaimValues.AllApplications)
                .Build();

            auth.AddPolicy(ApplicationPolicies.Administrator, op => op
                .RequireRole(ApplicationClaimValues.Administrator, ApplicationClaimValues.SuperAdmin)
                .RequireClaim(ClaimTypes.Application, ApplicationClaimValues.ApplicationName,
                    ApplicationClaimValues.AllApplications));

            auth.AddPolicy(ApplicationPolicies.Reviewer, op => op
                .RequireRole(ApplicationClaimValues.Reviewer, ApplicationClaimValues.Administrator,
                    ApplicationClaimValues.SuperAdmin)
                .RequireClaim(ClaimTypes.Application, ApplicationClaimValues.ApplicationName,
                    ApplicationClaimValues.AllApplications));
        });
        return services;
    }
}


