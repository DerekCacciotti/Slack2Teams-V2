using Microsoft.AspNetCore.Components;
using Slack2Teams.Shared.Interfaces;
using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Blazor.Components.Pages;

public partial class SlackChannels : ComponentBase
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    [Inject]
    private ISlackDataService _slackDataService { get; set; }
    [Inject]
    private IHttpContextAccessor _httpContextAccessor { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        if (_httpContextAccessor != null && _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("SlackToken", out var cookieValue))
        {
            var slackDataRequest = new SlackDataRequest()
            {
                Token = cookieValue
            };
            var channels = await _slackDataService.GetSlackChannelData(slackDataRequest);
        }
        
    }
}