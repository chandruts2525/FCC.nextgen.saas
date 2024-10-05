using FCC.Core.Constants;
using WorkManagement.DataAccess.Sql.DI;
using WorkManagement.Service.Handlers;
using WorkManagement.Service.Handlers.JobType;
using WorkManagement.Service.Handlers.ScheduleTypes;
using Softura.Web.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Host.AddAzureVault<Program>();
ConfigurationManager _configuration = builder.Configuration;

#region FCC injections
string? allowOrigin = _configuration[Keys.AllowOriginKey]?.ToString();
string? connString = _configuration[Keys.DatabaseKey]?.ToString();
builder.Services.AddIACDatabase(connString);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateJobTypesCommandHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUnitorComponentQueryHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateScheduleTypeCommendHandler).GetTypeInfo().Assembly)); 

#endregion

builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
	options.AddPolicy("CORSPolicy",
		builder => builder.WithOrigins(allowOrigin)
				   .SetIsOriginAllowedToAllowWildcardSubdomains()
		.AllowAnyMethod()
		.AllowAnyHeader()
		.AllowCredentials()
		.SetIsOriginAllowed((hosts) => true));
});
var app = builder.Build();


	app.UseSwagger();
	app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("CORSPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
