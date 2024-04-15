using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Application;
using Infrastructure;
using Newtonsoft.Json;
using Sieve.Models;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using TSR_WebUl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddWebUiServices(builder.Configuration);

builder.Services.Configure<SieveOptions>(builder.Configuration.GetSection("Sieve"));

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<DbMigration>();
    await initialiser.InitialiseAsync();
}

app.UseHealthChecks("/status", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        var result = new
        {
            Status = report.Status.ToString(),
            Checks = report.Entries.Select(entry => new
            {
                Name = entry.Key,
                Status = entry.Value.Status.ToString(),
                entry.Value.Description
            })
        };

        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CORS_POLICY");
app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
    settings.EnableTryItOut = true;
    settings.PersistAuthorization = true;
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => Results.Redirect("/api"));

app.Run();