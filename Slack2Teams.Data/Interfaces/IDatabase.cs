namespace Slack2Teams.Data.Interfaces;

public interface IDatabase
{
    public string Creator { get; set; }
    public DateTime CreateDate { get; set; }
    public string Editor { get; set; }
    public DateTime? EditDate { get; set; }
}