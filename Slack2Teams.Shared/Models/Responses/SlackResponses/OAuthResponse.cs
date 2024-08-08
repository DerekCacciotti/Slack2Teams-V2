namespace Slack2Teams.Shared.Models.Responses.SlackResponses;

public class OAuthResponse
{
    public bool ok { get; set; }
    public string access_token { get; set; }
    public string token_type { get; set; }
    public string scope { get; set; }
    public string bot_user_id { get; set; }
    public string app_id { get; set; }
    public Team team { get; set; }
    public Enterprise enterprise { get; set; }
    public AuthedUser authed_user { get; set; }
}

public class Team
{
    public string name { get; set; }
    public string id { get; set; }
}

public class AuthedUser
{
    public string id { get; set; }
    public string scope { get; set; }
    public string access_token { get; set; }
    public string token_type { get; set; }
}

public class Enterprise
{
    public string name { get; set; }
    public string id { get; set; }
}