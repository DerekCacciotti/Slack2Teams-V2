using Microsoft.EntityFrameworkCore;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Data.Models;
using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Api.Services;

public class SlackChannelStagingService : ISlackChannelStager
{
    private readonly Slack2TeamsContext _ctx;

    public SlackChannelStagingService(Slack2TeamsContext ctx)
    {
        _ctx = ctx;
    }

    public async Task StageSlackChannels(StageSlackChannelsRequest request)
    {
        if(!request.channels.Any())
        {
            return;
        }
        var channelIds = request.channels.Select(c => c.id).ToList();
        var alreadyStaged = await IsChannelStaged(channelIds, request.UserTenantInfo.TenantFK);
        if (alreadyStaged)
        {
            throw new ApplicationException("Channel already staged");
        }

        var stagedChannels = request.channels.Select(c => new StagedSlackChannel()
        {
            ChannelName = c.name,
            SourceID = c.id,
            ChannelDescription = !string.IsNullOrEmpty(c.purpose.value) ? c.purpose.value : !string.IsNullOrEmpty(c.topic.value) ? c.topic.value : null,
            SlackCreator = c.creator,
            SlackCreateDate = DateTimeOffset.FromUnixTimeSeconds(c.created).DateTime,
            isPrivate = c.is_private,
            isArchived = c.is_archived,
            Creator = request.CreatedBy,
            CreateDate = DateTime.Now,
            TenantFK = request.UserTenantInfo.TenantFK,
        }).ToList();

        await _ctx.SlackChannels.AddRangeAsync(stagedChannels);
        await _ctx.SaveChangesAsync();
    }
    
    private async Task<bool> IsChannelStaged(List<string> channelId, Guid tenantFK)
    {
        return await _ctx.SlackChannels.AnyAsync(c => channelId.Contains(c.SourceID) && c.TenantFK == tenantFK);
    }
}