using Microsoft.EntityFrameworkCore;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Data.Models;

namespace Slack2Teams.Api.Code;

public class SlackChannelLoader : ISlackChannelLoader
{
    private readonly Slack2TeamsContext _ctx;

    public SlackChannelLoader(Slack2TeamsContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<StagedSlackChannel> GetSlackChannel(string slackChannelId, Guid tenantFK)
    {
        var channel = await _ctx.SlackChannels.FirstOrDefaultAsync(x => x.SourceID == slackChannelId && x.TenantFK == tenantFK);
        if (channel != null)
        {
            return channel;
        }
        else
        { 
            return new StagedSlackChannel();
        }
    }
}