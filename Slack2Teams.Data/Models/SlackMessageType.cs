using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Slack2Teams.Data.Interfaces;

namespace Slack2Teams.Data.Models;

[Table("SlackMessageType")]
public class SlackMessageType : ICodeTable
{
    [Key]
    [Column(Order = 0)]
    public Guid SlackMessageTypePK { get; set; }
    public string Value { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsActive { get; set; }
}

