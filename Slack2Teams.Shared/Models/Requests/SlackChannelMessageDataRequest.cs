namespace Slack2Teams.Shared.Models.Requests;

public class SlackChannelMessageDataRequest : SlackDataRequest 
{
    public List<string> ChannelIds { get; set; }
}