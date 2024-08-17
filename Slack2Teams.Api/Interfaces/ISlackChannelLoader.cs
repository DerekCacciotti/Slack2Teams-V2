using Slack2Teams.Data.Models;

namespace Slack2Teams.Api.Interfaces;

public interface ISlackChannelLoader
{
    Task<StagedSlackChannel> GetSlackChannel(string slackChannelId, Guid tenantFK);
    
}