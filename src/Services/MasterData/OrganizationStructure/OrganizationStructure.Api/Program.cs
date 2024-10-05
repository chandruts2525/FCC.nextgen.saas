using FCC.Core.Constants;
using Softura.Web.Extensions;
using System.Reflection;
using OrganizationStructure.Service.Handlers.Organizations;
using OrganizationStructure.Service.Handlers.Yards;
using OrganizationStructure.DataAccess.Sql.DI; 
using OrganizationManagementService.Service.Handlers.Companies;
using IAC.Service.Handlers.Role;
using Softura.Azure.Storage.Blobs;
using OrganizationManagementService.Api;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Host.AddAzureVault<Program>();
ConfigurationManager _configuration = builder.Configuration;


#region FCC injections
string? allowOrigin = _configuration[Keys.AllowOriginKey]?.ToString();
string? connString = _configuration[Keys.DatabaseKey]?.ToString();
builder.Services.AddIACDatabase(connString);
builder.Services.AddBlobStorage<BlobStorageSetting>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrganizationCommandHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateOrganizationCommandHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetOrganizationQueryHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCompanyCommandHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAttachmentCommandHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateYardCommandHandler).GetTypeInfo().Assembly));
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
