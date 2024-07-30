using System.ComponentModel.DataAnnotations;
using Freepository.Models;

namespace Freepository.DTO_s;

public class CreateResourceDTO
{
    [Required]
    public string Title { get; set; } 
    [Required]
    public string Description { get; set; } 
    public string Url { get; set; }
    [Required]
    public string UserId { get; set; }
    // public List<TagDTO> Tags { get; set; } = [];

    public ICollection<int> TagIds { get; set; }
}