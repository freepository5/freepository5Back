using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freepository.Models;

public class Resource
{
    public int Id { get; set; } 
    public string Title { get; set; } 
    public string Description { get; set; } 
    public string Url { get; set; }
    public int UserId { get; set; }
    // public User User { get; set; }
    public ICollection<Tag> Tags { get; set; } = [];
}