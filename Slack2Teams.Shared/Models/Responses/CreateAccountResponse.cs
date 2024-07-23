using Slack2Teams.Shared.Interfaces;

namespace Slack2Teams.Shared.Models.Responses;

public class CreateAccountResponse : IApi
{
    public string UserName { get; set; }
    public bool IsSuccessful { get; set; }
    public List<string> Errors { get; set; }
}