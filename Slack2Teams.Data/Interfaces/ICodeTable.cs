namespace Slack2Teams.Data.Interfaces;

public interface ICodeTable
{
    public string Value { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsActive { get; set; }
}