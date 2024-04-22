using Application.Contracts.Word.Commands.Create;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using TSR_WebUl.Filters;
namespace TSR_WebUl
{
    public static class ConfigureServices
    {
        public static void AddWebUiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddHealthChecks();

            services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            })
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssembly(typeof(CreateWordCommand).Assembly);

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddEndpointsApiExplorer();
            services.AddCors();
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TSRApi",
                    Description = "TSRApi"
                });
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(BadRequestResponseFilter));
            });
            var corsAllowedHosts = configuration.GetSection("TSR-CORS").Get<string[]>();
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
