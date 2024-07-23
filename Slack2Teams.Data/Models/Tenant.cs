using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Slack2Teams.Data.Interfaces;

namespace Slack2Teams.Data.Models;
[Table("Tenant")]
public class Tenant: IDatabase
{
    [Key]
    [Column(Order = 0)]
    public Guid TenantPK { get; set; }
    public string UserFK { get; set; }
    public string TenantName { get; set; }
    public string SlackOAuthTokn { get; set; }
    public string Creator { get; set; }
    public DateTime CreateDate { get; set; }
    public string Editor { get; set; }
    public DateTime? EditDate { get; set; }
}