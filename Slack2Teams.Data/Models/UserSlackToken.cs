using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Slack2Teams.Data.Interfaces;

namespace Slack2Teams.Data.Models;
[Table("UserSlackToken")]
public class UserSlackToken : IDatabase
{
    [Key]
    [Column(Order = 0)]
    public Guid UserSlackTokenPK { get; set; }
    [Column(Order = 1)]
    public string SlackToken { get; set; }
    public string Creator { get; set; }
    public DateTime CreateDate { get; set; }
    public string Editor { get; set; }
    public DateTime? EditDate { get; set; }
    [Column(Order = 2)]
    public DateTime ExpirationDate { get; set; }
}