namespace Slack2Teams.Shared.Interfaces;

public interface IApi
{
    public bool IsSuccessful { get; set; }
    public List<string> Errors { get; set; }
    
}