using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Blazored.LocalStorage;
using Slack2Teams.Blazor.Interfaces;
using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Blazor.Services;

public class SlackMessageStagingService : ISlackMessageStagingService
{
    private readonly IHttpClientFactory _http;
    private readonly ILocalStorageService _localStorage;

    public SlackMessageStagingService(IHttpClientFactory http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    public async Task<bool> StageSlackMessage(List<StageSlackMessageRequest> requests)
    {
        var firstRequest = requests.FirstOrDefault();
        if (firstRequest == null)
        {
            return false;
        }

        var token = firstRequest.UserToken;

        try
        {
            var jsonConfig = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var client = _http.CreateClient("Slack2TeamsApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

           
            var tasks = requests.Select(async request =>
            {
                var json = JsonSerializer.Serialize(request, jsonConfig);
                await _localStorage.SetItemAsStringAsync("S2t_RawSlackData", json);
                var response = await client.PostAsync("Staging/StageSlackMessages", new StringContent(json, Encoding.UTF8, "application/json"));
                return response.IsSuccessStatusCode;
            });
            
            var results = await Task.WhenAll(tasks);

           
            return results.All(r => r);
        }
        catch
        {
            return false;
        }
    }
}
