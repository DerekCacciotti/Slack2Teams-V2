using Slack2Teams.Shared.Models.Responses.SlackResponses;

namespace Slack2Teams.Shared.Interfaces;

public interface ISlackApiCaller
{
    Task<SlackChannelResponse> GetSlackChannels(string token);
    Task<List<SlackMessageResponse>> GetSlackMessages(string token, List<string> channelIds);
    
    
}