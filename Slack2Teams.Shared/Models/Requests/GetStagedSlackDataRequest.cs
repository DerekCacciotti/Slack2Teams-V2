namespace Slack2Teams.Shared.Models.Requests;

public class GetStagedSlackDataRequest
{
    public Guid TenantFK { get; set; }
    public string SlackJson { get; set; }
}