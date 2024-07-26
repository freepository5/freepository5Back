using System.ComponentModel.DataAnnotations;
namespace Freepository.DTO_s;

public class RegisterDTO
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required] 
    public string? Password { get; set; }
}