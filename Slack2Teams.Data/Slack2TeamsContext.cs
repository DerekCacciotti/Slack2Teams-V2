using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Slack2Teams.Data.Models;
using Slack2Teams.Shared.Settings;

namespace Slack2Teams.Data;

public class Slack2TeamsContext: DbContext
{
    private readonly IOptions<AppSettings> _settings;
    
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<StagedSlackChannel> SlackChannels { get; set; }
    public DbSet<StagedSlackMessage> SlackMessages { get; set; }
    public DbSet<SlackMessageType> SlackMessageTypes { get; set; }
    

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
        modelBuilder.Entity<StagedSlackChannel>().Property(s => s.Editor).IsRequired(false);
        modelBuilder.Entity<StagedSlackChannel>().Property(s => s.ChannelDescription).IsRequired(false);
        modelBuilder.Entity<StagedSlackChannel>().Ignore(sm => sm.Messages);
        modelBuilder.Entity<StagedSlackMessage>().Property(sm => sm.Editor).IsRequired(false);
        modelBuilder.Entity<StagedSlackMessage>().Property(sm => sm.SlackTimeStamp);
        modelBuilder.Entity<StagedSlackMessage>().Ignore(sm => sm.Channel);
        modelBuilder.Entity<StagedSlackMessage>().Ignore(sm => sm.SlackMessageType);
        
        base.OnModelCreating(modelBuilder);
    }
}