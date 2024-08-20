using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

    public async Task<bool> StageSlackMessage(List<StageSlackMessageRequest> requests)
    {
        var token = string.Empty;
        var firstRequest = requests.FirstOrDefault();
        if (firstRequest != null)
        {
            token = firstRequest.UserToken;
        }
        try
        {
            var jsonConfig = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var client = _http.CreateClient("Slack2TeamsApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            foreach (var request in requests)
            {
                var json = JsonSerializer.Serialize(request, jsonConfig);
                var response = await client.PostAsync("Staging/StageSlackMessages", new StringContent(json, Encoding.UTF8, "application/json"));
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}
