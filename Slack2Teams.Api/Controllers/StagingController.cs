using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StagingController : ControllerBase
    {
        private readonly ISlackChannelStager _slackChannelStager;
        private readonly ISlackMessageStager _slackMessageStager;

        public StagingController(ISlackChannelStager slackChannelStager, ISlackMessageStager slackMessageStager)
        {
            _slackChannelStager = slackChannelStager;
            _slackMessageStager = slackMessageStager;
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
    }
}
