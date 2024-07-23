using Slack2Teams.Api.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Data.Models;

namespace Slack2Teams.Api.Services;

public class TenantService: ITenantService
{
    private readonly Slack2TeamsContext _ctx;

    public TenantService(Slack2TeamsContext ctx)
    {
        _ctx = ctx;
    }

    public async Task CreateTenant(string userFK, string name)
    {
        await using var ctx = _ctx;

        var tenant = new Tenant()
        {
            UserFK = userFK,
            TenantName = !string.IsNullOrEmpty(name) ? name : $"Conversion for User ID {userFK}",
            Creator = "Tenant Service",
            CreateDate = DateTime.Now
        };

        ctx.Tenants.Add(tenant);
        await ctx.SaveChangesAsync();


    }

    public async Task<Tenant> GetTenant(Guid tenantPK)
    {
        await using var ctx = _ctx;
        return await ctx.Tenants.FindAsync(tenantPK) ?? new Tenant();
    }

    public async Task RemoveTenant(Guid tenantPK)
    {
        await using var ctx = _ctx;
        var tenant = await ctx.Tenants.FindAsync(tenantPK);
        if (tenant != null)
        {
            ctx.Tenants.Remove(tenant);
            await ctx.SaveChangesAsync();
        }
    }
}