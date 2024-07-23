using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Slack2Teams.Auth.Interfaces;
using Slack2Teams.Auth.Models;
using Slack2Teams.Shared.Models;
using Slack2Teams.Shared.Models.Responses;
using Slack2Teams.Shared.Settings;

namespace Slack2Teams.Auth.Services;

public class AuthService: IAuth
{
    private readonly UserManager<Slack2TeamsUser> _userManager;
    private readonly SignInManager<Slack2TeamsUser> _signInManager;
    private readonly IOptions<AppSettings> _settings;

    public AuthService(UserManager<Slack2TeamsUser> userManager, SignInManager<Slack2TeamsUser> signInManager, IOptions<AppSettings> settings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _settings = settings;
    }

    public async Task<AuthResponse> CreateAccount(CreateAccountModel model)
    {
        var response = new AuthResponse();
        try
        {
            var user = new Slack2TeamsUser()
            {
                ContactFirstName = model.FirstName,
                ContactLastName = model.LastName,
                Email = model.EmailAddress,
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                
                response.IsSuccessful = true;
                response.AuxData.Add("UserID", user.Id);
            }
            else
            {
                response.IsSuccessful = false;
                foreach (var error in result.Errors)
                {
                    response.Errors.Add(error.Description);
                }
            }
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.Errors.Add(e.Message);
        }

        return response;
    }

    public async Task<AuthResponse> Login(LoginModel model)
    {
        var response = new AuthResponse();
        try
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                if (result.Succeeded)
                {
                    var authClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id)
                    };
                    var token = GetToken(authClaims);
                    response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    response.IsSuccessful = true;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Errors.Add("Error logging in. Please check your login credentials.");
                }
            }
            else
            {
                response.IsSuccessful = false;
                response.Errors.Add("User not found");
            }
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.Errors.Add(e.Message);
        }

        return response;
    }

    public async Task<AuthResponse> LogOut(ClaimsPrincipal claimsPrincipal)
    {
        var isLoggedIn = claimsPrincipal.Identity.IsAuthenticated;

        if (isLoggedIn)
        {
            await _signInManager.SignOutAsync();
            return new AuthResponse()
            {
                IsSuccessful = true
            };
        }

        return new AuthResponse()
        {
            IsSuccessful = false
        };
    }

    public async Task DeleteAccount(string userFK)
    {
        throw new NotImplementedException();
    }
    
    private JwtSecurityToken GetToken(List<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.JWT.Secret));
        var token = new JwtSecurityToken(
            issuer: _settings.Value.JWT.ValidIssuer,
            audience: _settings.Value.JWT.ValidAudience,
            expires: DateTime.Now.AddHours(10),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }
}