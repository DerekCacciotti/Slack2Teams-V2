namespace Slack2Teams.Shared.Settings;

public class AppSettings
{
    public DataSource DataSource { get; set; }
    public JWT JWT { get; set; }
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