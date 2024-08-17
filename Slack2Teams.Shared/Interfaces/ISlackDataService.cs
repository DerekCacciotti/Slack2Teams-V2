using Slack2Teams.Shared.Models.Requests;
using Slack2Teams.Shared.Models.Responses.SlackResponses;

namespace Slack2Teams.Shared.Interfaces;

public interface ISlackDataService
{
    public Task<SlackChannelResponse> GetSlackChannelData(SlackDataRequest slackDataRequest);
    public Task<List<SlackMessageResponse>> GetSlackMessageData(SlackChannelMessageDataRequest slackChannelMessageDataRequest);
}