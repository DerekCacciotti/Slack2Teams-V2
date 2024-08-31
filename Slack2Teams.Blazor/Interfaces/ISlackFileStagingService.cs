using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Blazor.Interfaces;

public interface ISlackFileStagingService
{
    Task<bool> StageSlackFiles(StageSlackFilesMessagesRequest request);
}