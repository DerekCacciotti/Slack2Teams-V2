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

    public async Task<List<SlackMessageResponse>> GetSlackMessages(string token, List<string> channelIds)
    {
        var messages = new List<SlackMessageResponse>();
        var client = _httpClientFactory.CreateClient("SlackApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        foreach (var channelId in channelIds) 
        {
            var postData = new Dictionary<string, string>
            {
                {"channel", channelId }
            };
            var content = new FormUrlEncodedContent(postData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = await client.PostAsync($"conversations.history", content);
            if (response.IsSuccessStatusCode)
            {
                var rawData = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<SlackMessageResponse>(rawData);
                data.channelid = channelId;
                messages.Add(data);
            }
            else
            {
                throw new Exception("Failed to get messages from Slack");
            }
            
        }
        return messages;
    }
}