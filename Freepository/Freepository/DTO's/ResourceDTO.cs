using Freepository.Models;

namespace Freepository.DTO_s;

public class ResourceDTO
{
    public int Id { get; set; } 
    public string Title { get; set; } 
    public string Description { get; set; } 
    public string Url { get; set; }
    public string UserId { get; set; }
    // public User User { get; set; }
    
    // public ICollection<ResourceTag> ResourceTags { get; set; }
}