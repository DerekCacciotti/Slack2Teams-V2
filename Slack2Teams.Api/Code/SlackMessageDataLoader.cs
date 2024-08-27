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

        var messagesIds = await _ctx.SlackChannels.Include(sc => sc.Messages)
            .Where(sc => sc.TenantFK == tenantFK)
            .SelectMany(sc => sc.Messages)
            .Where(x => (x.HasFile.HasValue && x.HasFile.Value))
            .Select(sc => sc.SlackMessagePK)
            
            .ToListAsync();


        return messagesIds;
    }
}