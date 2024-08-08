namespace Slack2Teams.Shared.Settings;

public class AppSettings
{
    public DataSource DataSource { get; set; }
    public JWT JWT { get; set; }
    public SharedSettings SharedSettings { get; set; }
}

public class DataSource
{
    public string Auth { get; set; }
    public string Data { get; set; }
    public string Logging { get; set; }
}

public class JWT
{
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
    public string Secret { get; set; }
}
public class SharedSettings
{
    public Slack Slack { get; set; }
    public Blazor Blazor { get; set; }
}

public class Slack
{
    public string ClientID { get; set; }
    public string ClientSecret { get; set; }
    public string BaseUrl { get; set; }
}

public class Blazor
{
    public string AppUrl { get; set; }
}