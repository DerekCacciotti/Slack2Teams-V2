using Slack2Teams.Shared.Interfaces;

namespace Slack2Teams.Shared.Models.Responses;

public class LoginResponse: IApi
{
    public bool IsSuccessful { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    public string Token { get; set; }
}