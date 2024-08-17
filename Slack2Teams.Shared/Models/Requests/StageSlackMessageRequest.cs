using Slack2Teams.Shared.Models.Responses.SlackResponses;

namespace Slack2Teams.Shared.Models.Requests;

public class StageSlackMessageRequest
{
    public List<SlackMessage> Messages { get; set; }
    public Guid TenantFK { get; set; }
    public string SlackChannelId { get; set; }
    public string UserToken { get; set; }
    
}