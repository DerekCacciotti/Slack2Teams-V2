using Microsoft.AspNetCore.Components;
using Radzen;

namespace Slack2Teams.Blazor.Components.Shared;

public partial class S2TButton : ComponentBase
{
    private bool showSpinner = false;
    [Parameter]
    public string Text { get; set; }
    [Parameter]
    public ButtonStyle ButtonColor { get; set; }
    [Parameter]
    public ButtonSize? Size { get; set; }
    [Parameter]
    public string Icon { get; set; }
    [Parameter]
    public string Styles { get; set; }
    [Parameter]
    public bool? ButtonDisabled { get; set; }
    [Parameter]
    public EventCallback OnClick { get; set; }

    public async Task OnClickAction()
    {
        await OnClick.InvokeAsync();
    }
}