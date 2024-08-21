using Slack2Teams.Data.Models;

namespace Slack2Teams.Api.Interfaces;

public interface ISlackMessageDataLoader
{
    Task<List<StagedSlackMessage>> GetStagedSlackMessagesForFileMigration(Guid tenantFK);
}