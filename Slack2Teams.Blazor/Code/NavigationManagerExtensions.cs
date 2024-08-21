using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Slack2Teams.Blazor.Code;

public static class NavigationManagerExtensions
{
    public static bool TryGetQueryString<T>(this NavigationManager navigationManager, string key, out T value)
    {
        var url = navigationManager.ToAbsoluteUri(navigationManager.Uri);
        if (QueryHelpers.ParseQuery(url.Query).TryGetValue(key, out var qvalues))
        {
            if (typeof(T) == typeof(int) && int.TryParse(qvalues, out var iqvalue))
            {
                value = (T)(object)iqvalue;
                return true;
            }

            if (typeof(T) == typeof(string))
            {
                value = (T)(object)qvalues.ToString();
                return true;
            }

            if (typeof(T) == typeof(decimal) && decimal.TryParse(qvalues, out var decvalue))
            {
                value = (T)(object)decvalue;
                return true;
            }

            if (typeof(T) == typeof(bool) && bool.TryParse(qvalues, out var bvalues))
            {
                value = (T)(object)bvalues;
                return true;
            }

            if (typeof(T) == typeof(Guid) && Guid.TryParse(qvalues, out var guidvalue))
            {
                value = (T)(object)guidvalue;
                return true;
            }
        }

        value = default;
        return false;
    }
}