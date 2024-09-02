using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Slack2Teams.Data.Interfaces;

namespace Slack2Teams.Data.Models;
[Table("SlackMessageFiles")]
public class StagedSlackFile: IDatabase
{
    [Key]
    [Column(Order = 0)]
    public Guid StagedSlackFilePK { get; set; }
    [Column(Order = 1)]
    public Guid StagedSlackMessageFK { get; set; }
    public virtual StagedSlackMessage SlackMessage { get; set; }
    [Column(Order = 2)]
    public string FileName { get; set; }
    [Column(Order = 3)]
    public string FileType { get; set; }
    [Column(Order = 4)]
    public string MimeType { get; set; }
    [Column(Order = 5)]
    public string SlackDownloadUrl { get; set; }
    [Column(Order = 6)]
    public bool? IsPublicSlackFile { get; set; }
    [Column(Order = 7)]
    public bool? IsSlackFileExternal { get; set; }
    public string SourceID { get; set; }
    public string SlackTimeStamp { get; set; }
    public string SlackCreator { get; set; }
    public DateTime? SlackFileCreateDate { get; set; }
    public string AzureBlobUrl { get; set;  }
    public string Creator { get; set; }
    public DateTime CreateDate { get; set; }
    public string Editor { get; set; }
    public DateTime? EditDate { get; set; }

    public StagedSlackFile()
    {
        StagedSlackFilePK = Guid.NewGuid();
    }
}