using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slack2Teams.Api.Interfaces;
using Slack2Teams.Shared.Models;
using Slack2Teams.Shared.Models.Responses;

namespace Slack2Teams.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        [HttpGet("GetTenantForUser")]
        public async Task<IActionResult> GetTenantForUser()
        {
            var userFK = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userFK))
            {
                return BadRequest();
            }

            var tenant = await _tenantService.GetTenantByUserFK(userFK);
            if (tenant == null)
            {
                return NotFound();
            }
            
            var response = new GetTenantResponse()
            {
                TenantPK = tenant.TenantPK,
                
            };

            return Ok(response);
        }
        
        
        
    }
}
