using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freepository.Models;

public class Resource
{
    public int Id { get; set; } 
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; } 
    public string Url { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }

    public int ModuleId { get; set; }
    public Module Module { get; set; }
    public ICollection<ResourceTag> ResourceTags { get; set; } = new List<ResourceTag>();
}