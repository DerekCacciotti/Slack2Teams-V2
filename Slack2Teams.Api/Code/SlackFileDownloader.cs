using System.Net.Http.Headers;
using Slack2Teams.Api.Interfaces;

namespace Slack2Teams.Api.Code;

public class SlackFileDownloader : ISlackFileDownloader
{
    private readonly IHttpClientFactory _http;

    public SlackFileDownloader(IHttpClientFactory http)
    {
        _http = http;
    }
    public async Task<Stream?> DownloadFileFromSlack(string url, string slackToken)
    {
        var fileClient = _http.CreateClient("SlackFiles");
        fileClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", slackToken);
        var response = await fileClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        { 
            var stream = await response.Content.ReadAsStreamAsync();
            return stream;
        }

        return null;
    }
}