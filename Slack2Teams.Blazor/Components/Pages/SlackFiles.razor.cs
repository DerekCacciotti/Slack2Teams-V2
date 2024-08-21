using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Slack2Teams.Blazor.Code;

namespace Slack2Teams.Blazor.Components.Pages;

public partial class SlackFiles : ComponentBase
{
    [CascadingParameter]
    private Task<AuthenticationState> authenticationStateTask { get; set; }
    [Inject]
    private NavigationManager _navigationManager { get; set; }

    private Guid TenantFK;

    protected override async Task OnInitializedAsync()
    {
        _navigationManager.TryGetQueryString("TenantFK", out TenantFK);
    }

    private async Task ImportFiles()
    {
        throw new NotImplementedException();
    }
}