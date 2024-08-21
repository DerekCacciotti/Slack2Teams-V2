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

    public async Task<List<StagedSlackMessage>> GetStagedSlackMessagesForFileMigration(Guid tenantFK)
    {
        if (tenantFK == Guid.Empty)
        {
            throw new ApplicationException("Error loading tenant");
        }
        
      
    }
}