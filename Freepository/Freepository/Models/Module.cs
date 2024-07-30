namespace Freepository.Models;

public class Module
{
    public int Id { get; set; }
    public string Title { get; set; }

    public ICollection<Resource> Resources { get; set; } = new List<Resource>();
}