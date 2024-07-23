using Slack2Teams.Shared.Interfaces;

namespace Slack2Teams.Shared.Models.Responses;

public class AuthResponse : IApi 
{
    public bool IsSuccessful { get; set; }
    public List<string> Errors { get; set; }
    public Dictionary<string, string> AuxData { get; set; }
    public string Token { get; set; }

    public AuthResponse()
    {
        Errors = new List<string>();
        AuxData = new Dictionary<string, string>();
    }
}