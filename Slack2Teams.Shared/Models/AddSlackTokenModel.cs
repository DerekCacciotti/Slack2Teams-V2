namespace Slack2Teams.Shared.Models;

public class AddSlackTokenModel
{
    public Guid TenantFK { get; set; }
    public string Token { get; set; }
    public string UserName { get; set; }
}