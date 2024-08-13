using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Data.Models;
using Slack2Teams.Shared.Models;
using Slack2Teams.Shared.Models.Responses.SlackResponses;
using Slack2Teams.Shared.Settings;

namespace Slack2Teams.Api.Managers;

public class SlackTokenManager: ISlackTokenManager
{
    private readonly Slack2TeamsContext _ctx;
    private readonly IOptions<AppSettings> _settings;
    private IHttpClientFactory _http;

    public SlackTokenManager(Slack2TeamsContext ctx, IOptions<AppSettings> settings, IHttpClientFactory http)
    {
        _ctx = ctx;
        _settings = settings;
        _http = http;
    }

    

    public async Task<string> GetSlackOAuthToken(string code)
    {
        var token = string.Empty;
        var client = _http.CreateClient("SlackApi");
        var postData = new Dictionary<string, string>
        {
            {"client_id", _settings.Value.SharedSettings.Slack.ClientID },
            {"client_secret", _settings.Value.SharedSettings.Slack.ClientSecret },
            {"code", code }
        };
        var content = new FormUrlEncodedContent(postData);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        var response = await client.PostAsync("oauth.access", content);
        if (response.IsSuccessStatusCode)
        {
            var rawdata = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<OAuthResponse>(await response.Content.ReadAsStringAsync());
           token = data.access_token;
        }

        return token;

    }
}