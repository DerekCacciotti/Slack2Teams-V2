using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Slack2Teams.Blazor.Interfaces;
using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Blazor.Services;

public class SlackChannelStagingService : ISlackChannelStagerService
{
    private readonly IHttpClientFactory _http;

    public SlackChannelStagingService(IHttpClientFactory http)
    {
        _http = http;
    }

    public async Task<bool> StageSlackChannelsForMigration(StageSlackChannelsRequest request)
    {
       var client = _http.CreateClient("Slack2TeamsApi");
       client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.UserToken);
       var json = JsonSerializer.Serialize(request);
       var response = await client.PostAsync("Staging/StageSlackChannels", new StringContent(json, Encoding.UTF8, "application/json"));
       return response.IsSuccessStatusCode;
    }
}