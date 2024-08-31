using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Slack2Teams.Blazor.Code;
using Slack2Teams.Blazor.Interfaces;
using Slack2Teams.Shared.Models;
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
    [Inject]
    private IStagedDataService _stagedDataService { get; set; }

    [Inject]
    private ISlackFileStagingService _slackFileStagingService { get; set; }

    [Inject] 
    private IUserTenantService _tenantService { get; set; }

    private Guid _tenantFk;
    private UserTenantInfo _tenantInfo;

    protected override async Task OnInitializedAsync()
    {
        _navigationManager.TryGetQueryString("TenantFK", out _tenantFk);
        _tenantInfo = await _tenantService.GetCurrentTenantInfo();

    }

    private async Task ImportFiles()
    {
        var messageJson = await _localStorage.GetItemAsStringAsync("S2t_RawSlackData");
        var authToken = await _localStorage.GetItemAsStringAsync("authToken");
        if (!string.IsNullOrEmpty(messageJson))
        {
            var request = new GetStagedSlackDataRequest()
            {
                TenantFK = _tenantFk,
                SlackJson = messageJson
            };
            var messagesWithFiles = await _stagedDataService.GetStagedMessages(request);

            var filesRequest = new StageSlackFilesMessagesRequest()
            {
                MessageIds = messagesWithFiles.MessageIds,
                SlackJson = messageJson,
                UserTenantInfo = _tenantInfo,
                CreatedBy = authenticationStateTask.Result.User.Identity.Name,
                UserToken = authToken
            };
            await _slackFileStagingService.StageSlackFiles(filesRequest);
        }
       
        

    }
}