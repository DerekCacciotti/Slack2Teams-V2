using Slack2Teams.Data.Models;

namespace Slack2Teams.Api.Interfaces;

public interface ITenantService
{
    Task CreateTenant(string userFK, string name);
    Task<Tenant> GetTenant(Guid tenantPK);
    Task RemoveTenant(Guid tenantPK);

}