using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Slack2Teams.Auth.Models;


public class Slack2TeamsUser : IdentityUser
{
    [Column(Order = 5)]
    [Required]
    public string ContactFirstName { get; set; }
    [Column(Order = 6)]
    [Required]
    public string ContactLastName { get; set; }
    [Required] 
    public string Creator { get; set; } = "IdentityService";

    [Required] 
    public DateTime CreateDate { get; set; } = DateTime.Now;
    
    public string Editor { get; set; }
    
    public DateTime? EditDate { get; set; }
}