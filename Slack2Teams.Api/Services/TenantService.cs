using Microsoft.EntityFrameworkCore;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Data.Models;
using Slack2Teams.Shared.Models;

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

    public async Task SaveSlackTokenToTenant(AddSlackTokenModel model)
    {
        
        if (string.IsNullOrEmpty(model.Token))
        {
            throw new ApplicationException("Slack token is empty check for error");
        }
        
        var userSlackToken = new UserSlackToken()
        {
            Creator = model.UserName,
            CreateDate = DateTime.Now,
            SlackToken = model.Token,
            ExpirationDate = DateTime.Now.AddHours(12)
        };
        
        await _ctx.UserSlackTokens.AddAsync(userSlackToken);
        await _ctx.SaveChangesAsync();
        await attachTokentoTenant(model.TenantFK, userSlackToken.UserSlackTokenPK, model.UserName);
    }

    public async Task<Tenant> GetTenantByUserFK(string userFK)
    {
        await using var ctx = _ctx;
        return await ctx.Tenants.FirstOrDefaultAsync(x => x.UserFK == userFK) ?? new Tenant();
    }

    private async Task attachTokentoTenant(Guid tenantFK, Guid userSlackTokenFK, string userName)
    {
        
        var tenant = await _ctx.Tenants.FindAsync(tenantFK);
        if (tenant != null)
        {
            tenant.SlackTokenFK = userSlackTokenFK;
            tenant.Editor = userName;
            tenant.EditDate = DateTime.Now;
            await _ctx.SaveChangesAsync();
        }
    }
    
    private async Task<bool> TenantExists(string userFK)
    {
        await using var ctx = _ctx;
        return await ctx.Tenants.AnyAsync(x => x.UserFK == userFK);
        
    }
  
}