namespace Slack2Teams.Shared.Settings;

public class BlazorSettings
{
    public Environment Environment { get; set; }
}

public class Environment
{
    public Local Local { get; set; }
}

public class Local
{
    public string Slack2TeamsApi { get; set; }
}