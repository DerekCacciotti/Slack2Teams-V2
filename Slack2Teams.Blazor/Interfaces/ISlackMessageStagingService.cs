using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Blazor.Interfaces;

public interface ISlackMessageStagingService
{
    Task<bool> StageSlackMessage(List<StageSlackMessageRequest> requests);
}