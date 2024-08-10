using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using Slack2Teams.Shared.Interfaces;
using Slack2Teams.Shared.Models.Requests;
using Slack2Teams.Shared.Models.Responses.SlackResponses;

namespace Slack2Teams.Shared.Services;

public class SlackDataService : ISlackDataService
{
    private readonly IHttpClientFactory _http;
    private readonly ILocalStorageService _localStorage;

    public SlackDataService(IHttpClientFactory http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }
    
    public async Task<SlackChannelResponse> GetSlackChannelData(SlackDataRequest slackDataRequest)
    {
        var s2tToken = await _localStorage.GetItemAsStringAsync("authToken");
        var json = JsonSerializer.Serialize(slackDataRequest);
        var client = _http.CreateClient("Slack2TeamsApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", s2tToken);
        var response = await client.PostAsync("SlackData/GetSlackChannels", new StringContent(json, Encoding.UTF8, "application/json"));
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<SlackChannelResponse>(content);
        return result;
    }
}