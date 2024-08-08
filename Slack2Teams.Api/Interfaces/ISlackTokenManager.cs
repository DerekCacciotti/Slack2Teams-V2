using Slack2Teams.Shared.Models;

namespace Slack2Teams.Api.Interfaces;

public interface ISlackTokenManager
{
    Task SaveSlackTokenToTenant(AddSlackTokenModel model);
    Task<string> GetSlackOAuthToken(string code);
}