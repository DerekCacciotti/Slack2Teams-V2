using Slack2Teams.Shared.Models;
using Slack2Teams.Shared.Models.Responses;

namespace Slack2Teams.Blazor.Interfaces;

public interface IUserTenantService
{
    Task<Guid> GetTenantIdForUser();
    Task<UserTenantInfo?> GetCurrentTenantInfo();
}