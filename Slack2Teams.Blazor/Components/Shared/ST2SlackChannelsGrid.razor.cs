using Microsoft.AspNetCore.Components;
using Slack2Teams.Shared.Models.Responses.SlackResponses;


namespace Slack2Teams.Blazor.Components.Shared;

public partial class ST2SlackChannelsGrid : ComponentBase
{
    [Parameter]
    public List<SlackChannel> SlackChannels { get; set; }
}