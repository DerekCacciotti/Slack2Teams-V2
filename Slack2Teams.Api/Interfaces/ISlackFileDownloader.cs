namespace Slack2Teams.Api.Interfaces;

public interface ISlackFileDownloader
{
    Task<Stream?> DownloadFileFromSlack(string url, string slackToken);
}