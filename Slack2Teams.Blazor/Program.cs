using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Slack2Teams.Shared.Interfaces;
using Slack2Teams.Blazor.Components;
using Slack2Teams.Shared.Providers;
using Slack2Teams.Shared.Services;
using Slack2Teams.Shared.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", false, true);
builder.Configuration.AddJsonFile("appsettings.Development.json", true, true);
var appSettings = new BlazorSettings();
builder.Services.Configure<BlazorSettings>(builder.Configuration.GetSection("BlazorSettings"));
var section = builder.Configuration.GetSection("BlazorSettings");
section.Bind(appSettings);
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService,  AuthService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpClient("Slack2TeamsApi",
    c => c.BaseAddress = new Uri(appSettings.Environment.Local.Slack2TeamsApi));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();