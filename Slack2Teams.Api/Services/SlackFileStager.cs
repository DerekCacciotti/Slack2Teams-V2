using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Data.Models;
using Slack2Teams.Shared.Models;
using Slack2Teams.Shared.Models.Requests;
using Slack2Teams.Shared.Models.Responses.SlackResponses;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Slack2Teams.Api.Services;

public class SlackFileStager : ISlackFileStager
{
    private readonly Slack2TeamsContext _ctx;
    private readonly ISlackFileDownloader _slackFileDownloader;

    public SlackFileStager(Slack2TeamsContext ctx, ISlackFileDownloader slackFileDownloader)
    {
        _ctx = ctx;
        _slackFileDownloader = slackFileDownloader;
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
        var slackData = JsonSerializer.Deserialize<StageSlackMessageRequest>(request.SlackJson);
        if (!slackData.Messages.Any())
        {
            throw new ApplicationException("Slack json has no data");
        }

        var rawMessages = slackData.Messages.Where(x => x.files != null)
            .Select(x => new
            {
                MessageText = x.text,
                SlackTimeStamp = x.ts,
                Files = x.files
            }).ToList();

        var filesToImported = rawMessages.Join(messages, rm => rm.MessageText,
            m => m.MesaageText, (rm, m) => new SlackFileImport()
            {
                MessageFK = m.SlackMessagePK,
                Files = rm.Files
            }
        ).ToList();

        foreach (var stagedFile in from data in filesToImported from file in data.Files select new StagedSlackFile()
                 {
                     StagedSlackMessageFK = data.MessageFK,
                     FileName = file.name,
                     SlackDownloadUrl = file.url_private,
                     FileType = file.filetype,
                     MimeType = file.mimetype,
                     SlackCreator = file.user,
                     SlackTimeStamp = file.created.ToString(),
                     SourceID = file.id,
                     Creator = request.CreatedBy,
                     CreateDate = DateTime.Now,
                     IsSlackFileExternal = file.is_external,
                     IsPublicSlackFile = file.is_public
                 })
        
            await _ctx.SlackFiles.AddAsync(stagedFile);
        
        await _ctx.SaveChangesAsync();
    }
}