using Slack2Teams.Shared.Models;
using Slack2Teams.Shared.Models.Responses;

namespace Slack2Teams.Shared.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginModel model);
    Task LogOut();
    Task<CreateAccountResponse> CreateAccount(CreateAccountModel model);

}