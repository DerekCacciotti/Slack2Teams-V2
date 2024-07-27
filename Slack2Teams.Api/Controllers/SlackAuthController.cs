using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Shared.Models;

namespace Slack2Teams.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SlackAuthController : ControllerBase
    {
        private readonly ISlackTokenManager _slackTokenManager;

        public SlackAuthController(ISlackTokenManager slackTokenManager)
        {
            _slackTokenManager = slackTokenManager;
        }

        [HttpPost("AddTokentoTenant")]
        public async Task<IActionResult> AddTokenToTenant(AddSlackTokenModel model)
        {
            try
            {
                await _slackTokenManager.SaveSlackTokenToTenant(model);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
