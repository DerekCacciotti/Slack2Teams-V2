using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Slack2Teams.Shared.Interfaces;
using Slack2Teams.Shared.Models.Responses.SlackResponses;

namespace Slack2Teams.Shared.Services;

public class SlackApiCaller: ISlackApiCaller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SlackApiCaller(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<SlackChannelResponse> GetSlackChannels(string token)
    {
        var client = _httpClientFactory.CreateClient("SlackApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("conversations.list");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<SlackChannelResponse>(content);
        if(result.ok == false)
        {
            throw new Exception("Failed to get channels from Slack");
        }
        return result;
    }
}