using System.ComponentModel.DataAnnotations;

namespace Slack2Teams.Shared.Models;

public class  CreateAccountModel
{
    [Required(ErrorMessage = "First Name is required.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    [MinLength(5, ErrorMessage = "Username must at least 5 characters long.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MaxLength(10, ErrorMessage = "Password can not be greater than 10 characters long.")]
    [MinLength(4, ErrorMessage = "Password must at least 4 characters long.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Email Address is required.")]
    [EmailAddress]
    public string EmailAddress { get; set; }
    
    
    [Required(AllowEmptyStrings = true)]
    public string TenantName { get; set; }
    
}