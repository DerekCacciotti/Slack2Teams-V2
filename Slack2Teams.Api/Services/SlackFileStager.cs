using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Shared.Models.Requests;
using Slack2Teams.Shared.Models.Responses.SlackResponses;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Slack2Teams.Api.Services;

public class SlackFileStager : ISlackFileStager
{
    private readonly Slack2TeamsContext _ctx;

    public SlackFileStager(Slack2TeamsContext ctx)
    {
        _ctx = ctx;
    }

    public async Task StageSlackFiles(StageSlackFilesMessagesRequest request)
    {
        if (!request.MessageIds.Any())
        {
            throw new ApplicationException("No messages for file import");
        }

        if (string.IsNullOrEmpty(request.SlackJson))
        {
            throw new ApplicationException("Data from slack is needed for the file import");
        }

        var messages = await _ctx.SlackMessages.Where(m => request.MessageIds.Contains(m.SlackMessagePK)).ToListAsync();
        var slackData = JsonSerializer.Deserialize<SlackMessageResponse>(request.SlackJson);
        if (!slackData.messages.Any())
        {
            throw new ApplicationException("Slack json has no data");
        }

        var rawMessages = slackData.messages;

        foreach (var data in messages)
        {
            var filedata = rawMessages.Where(rm => rm.ts == data.SlackTimeStamp).FirstOrDefault();
            if (filedata != null)
            {
                
            }
            
        }

    }
}