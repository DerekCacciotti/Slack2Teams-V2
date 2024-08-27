using System.Net.Http.Headers;
using System.Text;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using Slack2Teams.Blazor.Interfaces;
using Slack2Teams.Shared.Interfaces;
using Slack2Teams.Shared.Models.Requests;
using Slack2Teams.Shared.Models.Responses;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Slack2Teams.Blazor.Services;

public class StagedDataService: IStagedDataService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IHttpClientFactory _http;

    public StagedDataService(ILocalStorageService localStorage, IHttpClientFactory http)
    {
        _localStorage = localStorage;
        _http = http;
    }


    public async Task<StagedMessageResponse> GetStagedMessages(GetStagedSlackDataRequest request)
    {
        var token = await _localStorage.GetItemAsStringAsync("authToken");
        var json = JsonSerializer.Serialize(request);
        var clinet = _http.CreateClient("Slack2TeamsApi");
        clinet.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await clinet.PostAsync("Staging/GetStagedSlackData",
            new StringContent(json, Encoding.UTF8, "application/json"));
        if (!response.IsSuccessStatusCode)
        {
            return new StagedMessageResponse();
        }
        
        var content = await response.Content.ReadAsStringAsync();
        var stagedMessageResponse = JsonConvert.DeserializeObject<StagedMessageResponse>(content);
        if (stagedMessageResponse != null)
        {
            return stagedMessageResponse;
        }

        return new StagedMessageResponse();

    }
}