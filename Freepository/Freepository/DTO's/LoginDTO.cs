using System.ComponentModel.DataAnnotations;
namespace Freepository.DTO_s;

public class LoginDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
}
