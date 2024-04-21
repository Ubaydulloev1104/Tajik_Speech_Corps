global using System.Net.Http.Json;
using Blazorise;
using Blazorise.Bootstrap;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using System.Reflection;
using Microsoft.FeatureManagement;
using Microsoft.Extensions.DependencyInjection;
using TSR_Client.Services.ApplicationService;
using TSR_Client.Services.Auth;
using TSR_Client.Services.CategoryServices;
using TSR_Client.Services.Profile;
using TSR_Client;
using System;
using System.Net.Http;
using TSR_Client.Services.WordService;
using TSR_Client.Identity;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();


builder.Services.AddMudServices();
builder.Services.AddMatBlazor();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddScoped(_ =>
    new IdentityHttpClient { BaseAddress = new Uri(builder.Configuration["IdentityHttpClient:BaseAddress"]!) });
builder.Services.AddScoped(_ =>
    new HttpClient { BaseAddress = new Uri(builder.Configuration["HttpClient:BaseAddress"]!) });


builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IWordService, WordService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore(s =>
{
    s.AddPolicy(ApplicationPolicies.Applicant, ac => ac
        .RequireRole(ApplicationClaimValues.Applicant, ApplicationClaimValues.Reviewer,
            ApplicationClaimValues.Administrator, ApplicationClaimValues.SuperAdmin)
        .RequireClaim(ClaimTypes.Application, ApplicationClaimValues.ApplicationName,
            ApplicationClaimValues.AllApplications)
        .RequireClaim(ClaimTypes.Id).RequireClaim(ClaimTypes.Email).RequireClaim(ClaimTypes.Username));

    s.AddPolicy(ApplicationPolicies.Reviewer, ac => ac
        .RequireRole(ApplicationClaimValues.Reviewer, ApplicationClaimValues.Administrator,
            ApplicationClaimValues.SuperAdmin)
        .RequireClaim(ClaimTypes.Application, ApplicationClaimValues.ApplicationName,
            ApplicationClaimValues.AllApplications)
        .RequireClaim(ClaimTypes.Id).RequireClaim(ClaimTypes.Email).RequireClaim(ClaimTypes.Username));

    s.AddPolicy(ApplicationPolicies.Administrator, ac => ac
        .RequireRole(ApplicationClaimValues.Administrator, ApplicationClaimValues.SuperAdmin)
        .RequireClaim(ClaimTypes.Application, ApplicationClaimValues.ApplicationName,
            ApplicationClaimValues.AllApplications)
        .RequireClaim(ClaimTypes.Id).RequireClaim(ClaimTypes.Email).RequireClaim(ClaimTypes.Username));
});
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureFlags"));
await builder.Build().RunAsync();