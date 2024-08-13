using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Slack2Teams.Blazor.Interfaces;
using Slack2Teams.Shared;
using Slack2Teams.Shared.Interfaces;
using Slack2Teams.Shared.Models;
using Slack2Teams.Shared.Models.Requests;
using Slack2Teams.Shared.Models.Responses.SlackResponses;

namespace Slack2Teams.Blazor.Components.Pages;

public partial class SlackChannels : ComponentBase
{
    [CascadingParameter]
    private Task<AuthenticationState> authenticationStateTask { get; set; }
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    [Inject]
    private ISlackDataService _slackDataService { get; set; }
    [Inject]
    private IHttpContextAccessor _httpContextAccessor { get; set; }
    [Inject]
    private IUserTenantService _userTenantService { get; set; }
    [Inject]
    private ILocalStorageService _localStorageService { get; set; }

    private SlackChannelResponse _slackChannelResponse = new SlackChannelResponse();
    
    protected override async Task OnInitializedAsync()
    {
       
        
        if (_httpContextAccessor != null && _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("SlackToken", out var cookieValue))
        {
            var tenantPK = await _userTenantService.GetTenantIdForUser();
            if(tenantPK != Guid.Empty)
            {
                var tenantInfo = new UserTenantInfo()
                {
                    TenantFK = tenantPK,
                    UserName = authenticationStateTask.Result.User.Identity.Name,
                    Token = cookieValue
                };
                
                var tenantJson = Utilities.ConvertToJson(tenantInfo);
                await _localStorageService.SetItemAsStringAsync("S2TTenant", tenantJson);
            }
            var slackDataRequest = new SlackDataRequest()
            {
                Token = cookieValue
            };
         
            _slackChannelResponse = await _slackDataService.GetSlackChannelData(slackDataRequest);
        }
        
    }
}