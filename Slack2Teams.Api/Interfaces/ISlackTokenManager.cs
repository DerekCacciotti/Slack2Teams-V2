namespace Slack2Teams.Api.Interfaces;

public interface ISlackTokenManager
{
    Task SaveSlackTokenToTenant(Guid tenantFK, string token, string userName);
}