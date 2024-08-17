using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Slack2Teams.Blazor.Interfaces;
using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Blazor.Services;

public class SlackMessageStagingService : ISlackMessageStagingService
{
    private readonly IHttpClientFactory _http;

    public SlackMessageStagingService(IHttpClientFactory http)
    {
        _http = http;
    }
    public async Task<bool> StageSlackMessage(StageSlackMessageRequest request)
    {
        var clinet = _http.CreateClient("Slack2TeamsApi");
        clinet.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.UserToken);
        var json = JsonSerializer.Serialize(request);
        var response = await clinet.PostAsync("Staging/StageSlackMessages", new StringContent(json, Encoding.UTF8, "application/json"));
        return response.IsSuccessStatusCode;
    }
}