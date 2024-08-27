using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Slack2Teams.Blazor.Code;
using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Blazor.Components.Pages;

public partial class SlackFiles : ComponentBase
{
    [CascadingParameter]
    private Task<AuthenticationState> authenticationStateTask { get; set; }
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    [Inject]
    private ILocalStorageService _localStorage { get; set; }

    private Guid _tenantFk;

    protected override async Task OnInitializedAsync()
    {
        _navigationManager.TryGetQueryString("TenantFK", out _tenantFk);
    }

    private async Task ImportFiles()
    {
        var messageJson = await _localStorage.GetItemAsStringAsync("S2t_RawSlackData");
        if (!string.IsNullOrEmpty(messageJson))
        {
            var request = new GetStagedSlackDataRequest()
            {
                TenantFK = _tenantFk,
                SlackJson = messageJson
            };
        }
       
        

    }
}