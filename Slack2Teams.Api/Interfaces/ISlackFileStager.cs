using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Api.Interfaces;

public interface ISlackFileStager
{
    Task StageSlackFiles(StageSlackFilesMessagesRequest request);
}