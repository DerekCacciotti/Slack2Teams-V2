using Microsoft.EntityFrameworkCore;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Data.Models;

namespace Slack2Teams.Api.Code;

public class SlackMessageDataLoader : ISlackMessageDataLoader
{
    private readonly Slack2TeamsContext _ctx;

    public SlackMessageDataLoader(Slack2TeamsContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<Guid>> GetStagedSlackMessagesForFileMigration(Guid tenantFK)
    {
        if (tenantFK == Guid.Empty)
        {
            throw new ApplicationException("Error loading tenant");
        }

        var messagesIds = await _ctx.SlackChannels.Join(_ctx.SlackMessages, channel => channel.SlackChannelPK, messages => messages.ChannelFK,
                (c, m) => new 
                {
                    TenantFK = tenantFK,
                    ChannelFK = c.SlackChannelPK,
                    MessagePK = m.SlackMessagePK,
                    HasFile = m.HasFile
                })
            .Where(x => x.TenantFK == tenantFK && (x.HasFile.HasValue && x.HasFile == true))
            .Select(d => d.MessagePK)
            .ToListAsync();


        return messagesIds;
    }
}