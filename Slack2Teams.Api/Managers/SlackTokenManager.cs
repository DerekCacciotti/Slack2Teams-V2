using Slack2Teams.Api.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Data.Models;

namespace Slack2Teams.Api.Managers;

public class SlackTokenManager: ISlackTokenManager
{
    private readonly Slack2TeamsContext _ctx;

    public SlackTokenManager(Slack2TeamsContext ctx)
    {
        _ctx = ctx;
    }

    public async Task SaveSlackTokenToTenant(Guid tenantFk, string token, string userName)
    {
        if (string.IsNullOrEmpty(token))
        {
            // handle err btter
            throw new ApplicationException("Slack token is empty check for error");
        }
        

        var userSlackToken = new UserSlackToken()
        {
            Creator = userName,
            CreateDate = DateTime.Now,
            SlackToken = token,
            ExpirationDate = DateTime.Now.AddHours(12)
        };

        await _ctx.UserSlackTokens.AddAsync(userSlackToken);
        await _ctx.SaveChangesAsync();
       await attachTokenToTenant(tenantFk, userSlackToken.UserSlackTokenPK, userName);

    }

    private async Task attachTokenToTenant(Guid tenantFK, Guid userSlackTokenFK, string userName)
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
}