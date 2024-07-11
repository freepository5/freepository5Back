using DefaultNamespace;

namespace Freepository.DTO_s;

public class ResourceDTO
{
    public int Id { get; set; } 
    public string Title { get; set; } 
    public string Description { get; set; } 
    public string Url { get; set; }
    public string Tags { get; set; } 
    public int UserId { get; set; } 
    public UserDTO User { get; set; } 
}