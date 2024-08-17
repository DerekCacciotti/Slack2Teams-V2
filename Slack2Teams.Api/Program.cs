using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Slack2Teams.Auth;
using Slack2Teams.Auth.Models;
using Slack2Teams.Shared.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Slack2Teams.Api.Code;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Api.Managers;
using Slack2Teams.Api.Services;
using Slack2Teams.Auth.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Shared.Interfaces;
using Slack2Teams.Shared.Services;
using AuthService = Slack2Teams.Auth.Services.AuthService;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", false, true);
builder.Configuration.AddJsonFile("appsettings.Development.json", true, true);
var appsettings = new AppSettings();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var section = builder.Configuration.GetSection("AppSettings");
section.Bind(appsettings);
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(appsettings?.DataSource.Auth));
builder.Services.AddDbContext<Slack2TeamsContext>(options => options.UseSqlServer(appsettings.DataSource.Data));

builder.Services.AddIdentity<Slack2TeamsUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    }).AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = appsettings.JWT.ValidAudience,
            ValidIssuer = appsettings.JWT.ValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appsettings.JWT.Secret))
        };
    });
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<ISlackTokenManager, SlackTokenManager>();
builder.Services.AddHttpClient("SlackApi", sa => sa.BaseAddress = new Uri(appsettings.SharedSettings.Slack.BaseUrl));
builder.Services.AddScoped<ISlackApiCaller, SlackApiCaller>();
builder.Services.AddScoped<ISlackChannelStager, SlackChannelStagingService>();
builder.Services.AddScoped<ISlackMessageStager, SlackMessagesStagingService>();
builder.Services.AddScoped<ISlackChannelLoader, SlackChannelLoader>();
builder.Services.AddScoped<ISlackMessageTypeLoader, SlackMessageTypeLoader>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();