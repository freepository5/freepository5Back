using System.ComponentModel.DataAnnotations;
namespace Freepository.DTO_s;

public class LoginDTO
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    public string? Password { get; set; }
}
