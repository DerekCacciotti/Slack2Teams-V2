using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Blazor.Interfaces;

public interface ISlackChannelStagerService
{
    Task<bool> StageSlackChannelsForMigration(StageSlackChannelsRequest request);
}