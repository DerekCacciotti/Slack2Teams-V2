using Microsoft.AspNetCore.Components;

namespace Slack2Teams.Blazor.Components.Shared;

public partial class S2TCard : ComponentBase
{
    [Parameter]
    public string Title { get; set; }
    [Parameter]
    public RenderFragment Header { get; set; }
    [Parameter]
    public RenderFragment Content { get; set; }
    [Parameter]
    public RenderFragment ActionButtons { get; set; }
    
}