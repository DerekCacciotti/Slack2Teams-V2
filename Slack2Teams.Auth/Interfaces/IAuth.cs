using System.Security.Claims;
using Slack2Teams.Shared.Models;
using Slack2Teams.Shared.Models.Responses;

namespace Slack2Teams.Auth.Interfaces;

public interface IAuth
{
    Task<AuthResponse> CreateAccount(CreateAccountModel model);
    Task<AuthResponse> Login(LoginModel model);
    Task<AuthResponse> LogOut(ClaimsPrincipal claimsPrincipal);
    Task DeleteAccount(string userFK);
}