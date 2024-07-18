namespace Freepository.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Resource> Resources { get; set; } = [];
}