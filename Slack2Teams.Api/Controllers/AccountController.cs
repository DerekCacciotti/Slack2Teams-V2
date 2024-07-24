using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Auth.Interfaces;
using Slack2Teams.Shared.Models;
using Slack2Teams.Shared.Models.Responses;
using Slack2Teams.Shared.Settings;

namespace Slack2Teams.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuth _authService;
        private readonly ITenantService _tenantService;
        private readonly IOptions<AppSettings> _settings;

        public AccountController(IAuth authService, ITenantService tenantService, IOptions<AppSettings> settings)
        {
            _authService = authService;
            _tenantService = tenantService;
            _settings = settings;
        }

        [HttpPost("CreateAccount")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount(CreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.CreateAccount(model);
                if (response.IsSuccessful)
                {
                    var userId = response.AuxData.FirstOrDefault(x => x.Key == "UserID").Value;
                    if (!string.IsNullOrEmpty(userId))
                    {
                        await _tenantService.CreateTenant(userId, model.TenantName);
                    }

                }
                else
                {
                    return BadRequest(new CreateAccountResponse(){IsSuccessful = false, Errors = response.Errors});
                }
            }
            else
            {
                // validation here 
                return BadRequest(new CreateAccountResponse()
                    { IsSuccessful = false, 
                        Errors = ["error processing request"]
                    });
            }

            return Ok(new CreateAccountResponse()
            {
                IsSuccessful = true,
                Errors = null,
                UserName = model.UserName
            });
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var slack = _settings.Value.SharedSettings.Slack;
            if (ModelState.IsValid)
            {
                var result = await _authService.Login(model);
                if (result.IsSuccessful)
                {
                    Response.Cookies.Append("Slack2TeamsToken", result.Token);
                    return Ok(new LoginResponse()
                    {
                        IsSuccessful = true,
                        Errors = null,
                        Token = result.Token
                    });
                }
                else
                {
                    return BadRequest(new LoginResponse()
                    {
                        IsSuccessful = false,
                        Errors = result.Errors
                    });
                    
                }
            }
            else
            {
                return BadRequest(new LoginResponse()
                {
                    IsSuccessful = false,
                    Errors = new List<string>() { "error logging in" }
                });
            }
        }
    }
}
