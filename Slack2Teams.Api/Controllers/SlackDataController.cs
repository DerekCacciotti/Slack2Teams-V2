using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slack2Teams.Shared.Interfaces;
using Slack2Teams.Shared.Models.Requests;

namespace Slack2Teams.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SlackDataController : ControllerBase
    {
        private readonly ISlackApiCaller _slackApiCaller;

        public SlackDataController(ISlackApiCaller slackApiCaller)
        {
            _slackApiCaller = slackApiCaller;
        }
        [HttpPost("GetSlackChannels")]
        public async Task<IActionResult> GetSlackChannels(SlackDataRequest slackDataRequest)
        {
            try
            {
                
                if (string.IsNullOrEmpty(slackDataRequest.Token))
                {
                    return BadRequest("No token found");
                }
                var channels = await _slackApiCaller.GetSlackChannels(slackDataRequest.Token);
                return Ok(channels);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
