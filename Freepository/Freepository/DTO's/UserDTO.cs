using Freepository.Models;

namespace Freepository.DTO_s;

public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public ICollection<Resource> Resources { get; set; }
}