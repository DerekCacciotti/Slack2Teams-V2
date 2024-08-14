using Microsoft.AspNetCore.Components;
using Slack2Teams.Shared.Models.Responses.SlackResponses;


namespace Slack2Teams.Blazor.Components.Shared;

public partial class ST2SlackChannelsGrid : ComponentBase
{
    [Parameter]
    public List<SlackChannel> SlackChannels { get; set; }

    public List<SlackChannel> selectedChannels = new List<SlackChannel>();
    
    private void OnSelectionChanged(bool isSelected, SlackChannel product)
    {
        if (isSelected)
        {
            if (!selectedChannels.Contains(product))
            {
                selectedChannels.Add(product);
            }
        }
        else
        {
            if (selectedChannels.Contains(product))
            {
                selectedChannels.Remove(product);
            }
        }
    }
    public async Task<List<SlackChannel>> GetSelectedChannels()
    {
        return selectedChannels;
    }
}