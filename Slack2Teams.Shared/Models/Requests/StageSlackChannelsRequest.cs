using Slack2Teams.Shared.Models.Responses.SlackResponses;

namespace Slack2Teams.Shared.Models.Requests;

public class StageSlackChannelsRequest
{
    public List<SlackChannel> channels { get; set; }
    public UserTenantInfo UserTenantInfo { get; set; }
    public string CreatedBy { get; set; }
    public string UserToken { get; set; }
}