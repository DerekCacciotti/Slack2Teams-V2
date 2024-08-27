using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Slack2Teams.Data.Interfaces;

namespace Slack2Teams.Data.Models;
[Table("SlackChannels")]
public class StagedSlackChannel : IDatabase
{

    [Key] [Column(Order = 0)] 
    public Guid SlackChannelPK { get; set; }
    [Column(Order = 1)]
    public Guid TenantFK { get; set; }
    [Column(Order = 2)]
    public string SourceID {get; set;}
    [Column(Order = 3)]
    public string ChannelName { get; set; }
    [Column(Order = 4)]
    public string ChannelDescription { get; set; }
    public string SlackCreator { get; set; }
    public DateTime SlackCreateDate { get; set; }
    public bool? isPrivate { get; set; }
    public bool? isArchived { get; set; }
    public string Creator { get; set; }
    public DateTime CreateDate { get; set; }
    public string Editor { get; set; }
    public DateTime? EditDate { get; set; }
    public virtual List<StagedSlackMessage> Messages { get; set; }

    public StagedSlackChannel()
    {
        SlackChannelPK = Guid.NewGuid();
    }
    
}
