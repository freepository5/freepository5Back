using System.ComponentModel.DataAnnotations;

namespace Freepository.Models;

public class Tag
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public ICollection<ResourceTag> ResourceTags { get; set; } = new List<ResourceTag>();
}