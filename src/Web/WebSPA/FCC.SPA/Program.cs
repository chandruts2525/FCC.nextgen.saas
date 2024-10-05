using FCC.SPA.Abstractions;
using FCC.SPA.Helper;
using FCC.SPA.Services;
using FCC.Web;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Softura.Core.Extensions;
using Softura.Web.Authenticaion;
using Softura.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager _configuration = builder.Configuration;

builder.Host.AddAzureVault<Program>();
// Add services to the container.
builder.Services.AddControllersWithViews();
string[] allowOrigin = _configuration.GetSection(Keys.AllowOriginKey).Get<string[]>();
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

builder.Services.AddTransient<IRestClientService, RestClientService>();
builder.Services.AddTransient<IApiService, ApiService>();
builder.Services.AddTransient<ILogService, LogService>();

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/build";
});


///Auth Code
var ssoclientid = _configuration[Keys.ClientId];
var ssotenantid = _configuration[Keys.TenantId];

builder.Services.AddSofturaMVCWithAADSSO(ssoclientid, ssotenantid, "softura.org");
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(Convert.ToDouble(_configuration[Keys.SessionTime]));
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

RestClientHelper.RestClientHelperConfig(app.Services.GetRequiredService<IConfiguration>());


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors("CORSPolicy");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseRouting();

//Auth code
app.UseCookiePolicy();

app.UseAuthentication();


////
app.UseAuthorization();
app.UseSession();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
    if (app.Environment.IsLocal())
    {
        spa.UseReactDevelopmentServer(npmScript: "start");
    }
});

app.Run();
