using Microsoft.EntityFrameworkCore;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Data.Models;

namespace Slack2Teams.Api.Code;

public class SlackMessageTypeLoader :ISlackMessageTypeLoader
{
    private readonly Slack2TeamsContext _ctx;

    public SlackMessageTypeLoader(Slack2TeamsContext ctx)
    {
        _ctx = ctx;
    }

   

    public async Task<SlackMessageType> GetOrCreateMessageType(string slackMessageType)
    {
        if (await _ctx.SlackMessageTypes.AnyAsync(mt => mt.Value == slackMessageType && mt.IsActive))
        {
            return await _ctx.SlackMessageTypes.FirstOrDefaultAsync(x => x.Value == slackMessageType && x.IsActive);
        }
        else
        {
            var newMessageType = new SlackMessageType()
            {
                Value = slackMessageType,
                CreateDate = DateTime.Now,
                IsActive = true,
            };

            await _ctx.SlackMessageTypes.AddAsync(newMessageType);
            await _ctx.SaveChangesAsync();
            return newMessageType;
        }
    }
}