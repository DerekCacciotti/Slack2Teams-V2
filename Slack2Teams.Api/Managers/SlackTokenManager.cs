using Slack2Teams.Api.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Data.Models;
using Slack2Teams.Shared.Models;

namespace Slack2Teams.Api.Managers;

public class SlackTokenManager: ISlackTokenManager
{
    private readonly Slack2TeamsContext _ctx;

    public SlackTokenManager(Slack2TeamsContext ctx)
    {
        _ctx = ctx;
    }

    public async Task SaveSlackTokenToTenant(AddSlackTokenModel model)
    {
        if (string.IsNullOrEmpty(model.Token))
        {
            // handle err btter
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
       await attachTokenToTenant(model.TenantFK, userSlackToken.UserSlackTokenPK, model.UserName);

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