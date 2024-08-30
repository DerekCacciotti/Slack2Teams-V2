namespace Slack2Teams.Shared.Models.Requests;

public class StageSlackFilesMessagesRequest
{
    public List<Guid> MessageIds { get; set; }
    public string SlackJson { get; set; }
    public UserTenantInfo UserTenantInfo { get; set; }
    public string CreatedBy { get; set; }
    public string UserToken { get; set; }
    
}