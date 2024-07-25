using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Slack2Teams.Data.Models;
using Slack2Teams.Shared.Settings;

namespace Slack2Teams.Data;

public class Slack2TeamsContext: DbContext
{
    private readonly IOptions<AppSettings> _settings;
    
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<UserSlackToken> UserSlackTokens { get; set; }

    public Slack2TeamsContext()
    {
        
    }

    public Slack2TeamsContext(DbContextOptions<Slack2TeamsContext> options, IOptions<AppSettings> settings): base(options)
    {
        _settings = settings;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_settings.Value.DataSource.Data);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>().Property(t => t.Editor).IsRequired(false);
        modelBuilder.Entity<Tenant>().Ignore(t => t.SlackToken);
        modelBuilder.Entity<UserSlackToken>().Property(st => st.Editor).IsRequired(false);
        base.OnModelCreating(modelBuilder);
    }
}