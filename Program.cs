using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using MLT.Rifa2.MVC.Interfaces;
using MLT.Rifa2.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

//politica usuarios autenticados
var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

//builder.Services.AddControllersWithViews(opciones =>
//{
//    opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
//});
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opciones =>
//    {
//        opciones.LoginPath = "/LogIn/LogIn";
//        opciones.LogoutPath = "/LogIn/LogOut";
//        opciones.AccessDeniedPath = "/LogIn/AccessDenied";
//    });
//builder.Services.AddAuthorization(opciones =>
//{
//    opciones.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IOrganizationTypeService, OrganizationTypeService>(c =>
{
    c.Timeout = TimeSpan.FromMinutes(5);
    c.BaseAddress = new Uri($"{configuration.GetValue<string>("APIIntegration:Path")}OrganizationType/");
});
builder.Services.AddHttpClient<IOrganizationService, OrganizationService>(c =>
{
    c.Timeout = TimeSpan.FromMinutes(5);
    c.BaseAddress = new Uri($"{configuration.GetValue<string>("APIIntegration:Path")}Organization/");
});
builder.Services.AddHttpClient<IGenericService, GenericService>(c =>
{
    c.Timeout = TimeSpan.FromMinutes(5);
    c.BaseAddress = new Uri($"{configuration.GetValue<string>("APIIntegration:Path")}Generic/");
});
builder.Services.AddHttpClient<IReferentService, ReferentService>(c =>
{
    c.Timeout = TimeSpan.FromMinutes(5);
    c.BaseAddress = new Uri($"{configuration.GetValue<string>("APIIntegration:Path")}Referent/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
