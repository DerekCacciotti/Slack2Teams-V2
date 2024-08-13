namespace Slack2Teams.Shared;
using System.Text.Json;
public class Utilities
{
    public static string ConvertToJson(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}