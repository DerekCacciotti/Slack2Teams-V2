using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Slack2Teams.Data.Interfaces;
using Slack2Teams.Shared.Models.Responses.SlackResponses;

namespace Slack2Teams.Data.Models;
[Table("SlackMessages")]
public class StagedSlackMessage : IDatabase
{
    public StagedSlackMessage()
    {
        SlackMessagePK = Guid.NewGuid();
    }
    
    [Key]
    [Column(Order = 0)]
    public Guid SlackMessagePK { get; set; }
    [Column(Order = 1)]
    public Guid ChannelFK { get; set; }
    public virtual StagedSlackChannel Channel { get; set; }
    
    [Column(Order = 2)]
    public string MesaageText { get; set; }
   
    [Column(Order = 3)]
    public string SlackTimeStamp { get; set; }
    [Column(Order = 4)]
    public DateTime? SlackCreateDate { get; set; }
    public Guid SlackMessageTypeFK { get; set; }
    public SlackMessageType SlackMessageType { get; set; }
    public bool? HasFile { get; set; }
    public string Creator { get; set; }
    public DateTime CreateDate { get; set; }
    public string Editor { get; set; }
    public DateTime? EditDate { get; set; }

  
}