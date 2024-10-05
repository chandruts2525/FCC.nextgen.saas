using FCC.Core.Constants;
using IAC.Api;
using IAC.DataAccess.Sql.DI;
using IAC.Service.Handlers.Role;
using IAC.Service.Handlers.User;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Softura.Azure.Storage.Blobs;
using Softura.Excel;
using Softura.Excel.Abstractions;
using Softura.PDF;
using Softura.PDF.Abstractions;
using Softura.Web.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddAzureVault<Program>();
ConfigurationManager _configuration = builder.Configuration;


#region FCC injections
string? allowOrigin = _configuration[Keys.AllowOriginKey]?.ToString();
string? connString = _configuration[Keys.DatabaseKey]?.ToString();
builder.Services.AddIACDatabase(connString);
builder.Services.AddBlobStorage<BlobStorageSetting>();
builder.Services.TryAddScoped<IExcelService, ExcelService>();
builder.Services.TryAddScoped<IPdfService, PdfService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateRoleCommandHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateRoleCommandHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SearchRoleQueryHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetSecurityUserListQueryHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllUserByBusinessEntityQueryHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateSecurityUserCommandHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateSecurityUserCommandHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUserQueryHandler).GetTypeInfo().Assembly)); 
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetBusinessEntityQueryHandler).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAttachmentCommandHandler).GetTypeInfo().Assembly));

#endregion

builder.Services.AddControllers();
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
