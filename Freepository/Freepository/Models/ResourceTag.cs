namespace Freepository.Models;

public class ResourceTag
{
    public int ResourceId { get; set; }
    public Resource Resource { get; set; }
    
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}