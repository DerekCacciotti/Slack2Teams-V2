using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Slack2Teams.Blazor.Components.Shared;
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
    [Inject]
    private ISlackChannelStagerService _slackChannelStagerService { get; set; }
    [Inject]
    private ISlackMessageStagingService _slackMessageStagingService { get; set; }

    private SlackChannelResponse _slackChannelResponse = new SlackChannelResponse();
    private ST2SlackChannelsGrid _slackChannelsGrid { get; set; }
    private bool hasError = false;
    
    
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

    private async Task GatherDataForImport()
    {
        hasError = false;
        var selectedChannels = await _slackChannelsGrid.GetSelectedChannels();
        if (!selectedChannels.Any())
        {
            hasError = true;
        }
        var tenantJson = await _localStorageService.GetItemAsStringAsync("S2TTenant");
        var authToken = await _localStorageService.GetItemAsStringAsync("authToken");
        if (!string.IsNullOrEmpty(tenantJson) && !string.IsNullOrEmpty(authToken))
        {
            var tenantInfo = JsonConvert.DeserializeObject<UserTenantInfo>(tenantJson); 
            var stageRequest = new StageSlackChannelsRequest()
            {
               UserToken = authToken,
                UserTenantInfo = tenantInfo,
                CreatedBy = authenticationStateTask.Result.User.Identity.Name,
                channels = selectedChannels
            };
            var result = await _slackChannelStagerService.StageSlackChannelsForMigration(stageRequest);
            if (result)
            {
             var selectedChannelIds = selectedChannels.Select(x => x.id).ToList();
             var responses = await GetMessages(selectedChannelIds, tenantInfo);
             var messages = responses.SelectMany(r => r.messages).ToList();
             var request = new StageSlackMessageRequest()
             {
                 Messages = messages,
                 TenantFK = tenantInfo.TenantFK,
                 UserToken = authToken
             };
             var messageResult = await _slackMessageStagingService.StageSlackMessage(request);
             if (messageResult)
             {
                 // redirect for teams 
             }
            }
            else
            {
                hasError = true;
            }
        }
        
    }
    
    private async Task<List<SlackMessageResponse>> GetMessages(List<string> channelIds, UserTenantInfo tenantInfo)
    {
        ;
        var slackChannelMessageDataRequest = new SlackChannelMessageDataRequest()
        {
            Token = tenantInfo.Token,
            ChannelIds = channelIds
        };
        return await _slackDataService.GetSlackMessageData(slackChannelMessageDataRequest);
    }
}