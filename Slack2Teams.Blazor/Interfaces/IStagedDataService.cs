using Slack2Teams.Shared.Models.Requests;
using Slack2Teams.Shared.Models.Responses;

namespace Slack2Teams.Blazor.Interfaces;

public interface IStagedDataService
{
    Task<StagedMessageResponse> GetStagedMessages(GetStagedSlackDataRequest request);
}