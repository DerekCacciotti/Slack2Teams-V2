using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Slack2Teams.Auth.Models;
using Slack2Teams.Shared.Settings;

namespace Slack2Teams.Auth;

public class AuthDbContext : IdentityDbContext<Slack2TeamsUser>
{
    private readonly IOptions<AppSettings> _settings;
    
    public AuthDbContext()
    {
        
    }

    public AuthDbContext(DbContextOptions<AuthDbContext> options, IOptions<AppSettings> appsettings) : base(options)
    {
        _settings = appsettings;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("Server=localhost;Database=Slack2TeamsAuth;User Id=sa;Password=Asking1234567@;TrustServerCertificate=true");
        optionsBuilder.UseSqlServer(_settings.Value.DataSource.Auth);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Slack2TeamsUser>().Property(u => u.Editor).IsRequired(false);
        base.OnModelCreating(builder);
    }
}