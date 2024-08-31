using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Data;
using Slack2Teams.Data.Models;
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

        var rawMessages = slackData.Messages.Where(x => x.files != null || x.files.Count != 0);

        foreach (var data in messages)
        {
            var filedata = rawMessages.Where(rm => rm.ts == data.SlackTimeStamp).FirstOrDefault();
            if ( filedata != null && filedata.files.Any())
            {
                var filestoDownload = filedata.files.Select(x => x.url_private).ToList();
                foreach (var fileUrl in filestoDownload)
                {
                    await _slackFileDownloader.DownloadFileFromSlack(fileUrl, request.UserTenantInfo.Token);
                }
                var slackfiles = filedata.files.Select(sf => new StagedSlackFile()
                {
                    FileName = sf.name,
                    SlackMessage = data,
                    StagedSlackMessageFK = data.SlackMessagePK,
                    FileType = sf.mimetype,
                    SourceID = sf.id,
                    SlackTimeStamp = !string.IsNullOrEmpty(sf.timestamp.ToString()) ? sf.timestamp.ToString() : sf.created.ToString(),
                    SlackDownloadUrl = sf.url_private,
                    IsPublicSlackFile = sf.is_public,
                    IsSlackFileExternal = sf.is_external,
                    CreateDate = DateTime.Now,
                    Creator = data.Creator,
                    SlackCreator = sf.user
                }).ToList();
                await _ctx.SlackFiles.AddRangeAsync(slackfiles);
                await _ctx.SaveChangesAsync();

                
                

            }
            
        }

    }
}