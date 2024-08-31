using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Slack2Teams.Blazor.Interfaces;
using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Blazor.Services;

public class SlackFileStagingService : ISlackFileStagingService
{
    private readonly IHttpClientFactory _http;

    public SlackFileStagingService(IHttpClientFactory http)
    {
        _http = http;
    }

    public async Task<bool> StageSlackFiles(StageSlackFilesMessagesRequest request)
    {
        var token = request.UserToken;
        var json = JsonSerializer.Serialize(request);
        var clinet = _http.CreateClient("Slack2TeamsApi");
        clinet.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await clinet.PostAsync("Staging/StageSlackFile",
            new StringContent(json, Encoding.UTF8, "application/json"));
        return response.IsSuccessStatusCode;
    }
}