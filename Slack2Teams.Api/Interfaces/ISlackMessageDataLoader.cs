using Slack2Teams.Data.Models;

namespace Slack2Teams.Api.Interfaces;

public interface ISlackMessageDataLoader
{
    Task<List<Guid>> GetStagedSlackMessagesForFileMigration(Guid tenantFK);
}