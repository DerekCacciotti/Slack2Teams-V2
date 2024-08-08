using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Shared.Models;
using Slack2Teams.Shared.Settings;

namespace Slack2Teams.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SlackAuthController : ControllerBase
    {
        private readonly ISlackTokenManager _slackTokenManager;
        private readonly IOptions<AppSettings> _settings;

        public SlackAuthController(ISlackTokenManager slackTokenManager, IOptions<AppSettings> settings)
        {
            _slackTokenManager = slackTokenManager;
            _settings = settings;
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
        [HttpGet("OAuthToken")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSlackOAuthToken([FromQuery] string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    return BadRequest();
                }
                
                var token = await _slackTokenManager.GetSlackOAuthToken(code);
                Response.Cookies.Append("SlackToken", token, new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.Now.AddHours(12)
                });
                return RedirectPermanent(_settings.Value.SharedSettings.Blazor.AppUrl);
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
    }
}
