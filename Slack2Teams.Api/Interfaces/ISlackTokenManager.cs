using Slack2Teams.Shared.Models;

namespace Slack2Teams.Api.Interfaces;

public interface ISlackTokenManager
{
    Task<string> GetSlackOAuthToken(string code);
}