using Slack2Teams.Data.Models;

namespace Slack2Teams.Api.Interfaces;

public interface ISlackMessageTypeLoader
{
    Task<SlackMessageType> GetOrCreateMessageType(string slackMessageType);
}