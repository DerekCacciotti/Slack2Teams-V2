using Slack2Teams.Api.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Data.Models;
using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Api.Services;

public class SlackMessagesStagingService : ISlackMessageStager
{
    private readonly Slack2TeamsContext _ctx;
    private readonly ISlackChannelLoader _slackChannelLoader;
    private readonly ISlackMessageTypeLoader _slackMessageTypeLoader;

    public SlackMessagesStagingService(Slack2TeamsContext ctx, ISlackChannelLoader slackChannelLoader, ISlackMessageTypeLoader slackMessageTypeLoader)
    {
        _ctx = ctx;
        _slackChannelLoader = slackChannelLoader;
        _slackMessageTypeLoader = slackMessageTypeLoader;
    }
    public async Task StageSlackMessages(StageSlackMessageRequest request)
    {
        var messageType = new SlackMessageType();
        if (!request.Messages.Any())
        {
             return;
        }
        if(request.TenantFK == Guid.Empty)
        {
            return;
        }
        if(string.IsNullOrEmpty(request.SlackChannelId))
        {
            return;
        }
        
        var slackChannel = await _slackChannelLoader.GetSlackChannel(request.SlackChannelId, request.TenantFK);
        if (slackChannel == null)
        {
            return;
        }

        var messageTypes = request.Messages.Select(ms => ms.type).ToList();
        
        foreach(var _messageType in messageTypes)
        {
            messageType = await _slackMessageTypeLoader.GetOrCreateMessageType(_messageType);
        }

        var stagedmessages = request.Messages.Select(m => new StagedSlackMessage
        {
            MesaageText = m.text,
            SlackTimeStamp = m.ts,
            //SlackCreateDate = m.ts.HasValue ? DateTimeOffset.FromUnixTimeSeconds(m.ts.Value).DateTime : null,
            Channel = slackChannel,
            ChannelFK = slackChannel.SlackChannelPK,
            Creator = slackChannel.Creator,
            CreateDate = DateTime.Now,
            SlackMessageType = messageType,
            SlackMessageTypeFK = messageType.SlackMessageTypePK
        }).ToList();

        await _ctx.SlackMessages.AddRangeAsync(stagedmessages);
        await _ctx.SaveChangesAsync();

    }
}