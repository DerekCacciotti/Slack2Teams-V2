using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Slack2Teams.Blazor.Interfaces;
using Slack2Teams.Shared.Interfaces;
using Slack2Teams.Shared.Models;
using Slack2Teams.Shared.Models.Requests;

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
    
    protected override async Task OnInitializedAsync()
    {
       
        
        if (_httpContextAccessor != null && _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("SlackToken", out var cookieValue))
        {
            var tenantPK = await _userTenantService.GetTenantIdForUser();
            if(tenantPK != Guid.Empty)
            {
                var addSlackTokenModel = new AddSlackTokenModel()
                {
                    TenantFK = tenantPK,
                    UserName = authenticationStateTask.Result.User.Identity.Name,
                    Token = cookieValue
                };
                await _userTenantService.SaveSlackTokenToTenant(addSlackTokenModel);
            }
            var slackDataRequest = new SlackDataRequest()
            {
                Token = cookieValue
            };
            var channels = await _slackDataService.GetSlackChannelData(slackDataRequest);
        }
        
    }
}