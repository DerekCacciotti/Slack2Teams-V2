using Slack2Teams.Shared.Models.Responses.SlackResponses;

namespace Slack2Teams.Shared.Models;

public class SlackFileImport
{
    public Guid MessageFK { get; set; }
    public List<SlackFile> Files { get; set; }
}