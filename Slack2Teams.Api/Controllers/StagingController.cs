using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Shared.Models.Requests;
using Slack2Teams.Shared.Models.Responses;

namespace Slack2Teams.Api.Controllers
{
   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StagingController : ControllerBase
    {
        private readonly ISlackChannelStager _slackChannelStager;
        private readonly ISlackMessageStager _slackMessageStager;
        private readonly ISlackMessageDataLoader _messageDataLoader;
        private readonly ISlackFileStager _slackFileStager;

        public StagingController(ISlackChannelStager slackChannelStager, ISlackMessageStager slackMessageStager, ISlackMessageDataLoader messageDataLoader, ISlackFileStager slackFileStager)
        {
            _slackChannelStager = slackChannelStager;
            _slackMessageStager = slackMessageStager;
            _messageDataLoader = messageDataLoader;
            _slackFileStager = slackFileStager;
        }

        [HttpPost("StageSlackChannels")]
        public async Task<IActionResult> StageSlackChannels(StageSlackChannelsRequest request)
        {
            try
            {
                await _slackChannelStager.StageSlackChannels(request);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpPost("StageSlackMessages")]
        public async Task<IActionResult> StageSlackMessages(StageSlackMessageRequest request)
        {
            try
            {
                await _slackMessageStager.StageSlackMessages(request);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost("GetStagedSlackData")]
        public async Task<IActionResult> GetStagedSlackData(GetStagedSlackDataRequest request)
        {
            try
            {
                var messagesIds = await _messageDataLoader.GetStagedSlackMessagesForFileMigration(request.TenantFK);
                return Ok(new StagedMessageResponse()
                {
                    MessageIds = messagesIds
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        [HttpPost("StageSlackFile")]
        public async Task<IActionResult> StageSlackFile(StageSlackFilesMessagesRequest request)
        {
            try
            {
                await _slackFileStager.StageSlackFiles(request);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
