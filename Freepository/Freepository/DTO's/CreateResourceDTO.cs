using Freepository.Models;

namespace Freepository.DTO_s;

public class CreateResourceDTO
{
    public string Title { get; set; } 
    public string Description { get; set; } 
    public string Url { get; set; }
    public CreateTagDTO Tag { get; set; }
    
    // public ICollection<ResourceTag> ResourceTags { get; set; }
}