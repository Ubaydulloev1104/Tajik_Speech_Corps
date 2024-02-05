using FluentValidation.AspNetCore;
using TSR_Accoun_Api.Filters;
using TSR_Accoun_Application;
using TSR_Accoun_Infrastructure;
using TSR_Accoun_Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
	options.Filters.Add<ApiExceptionFilterAttribute>();
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

WebApplication app = builder.Build();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
using (var scope = app.Services.CreateScope())
{
	var initialiser = scope.ServiceProvider.GetRequiredService<DbMigration>();
	await initialiser.InitialiseAsync();
}
var applicationDbContextInitializer = app.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
await applicationDbContextInitializer.SeedAsync();

app.UseCors("CORS_POLICY");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
