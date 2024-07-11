namespace DefaultNamespace;

public class Resource
{
    public int Id { get; set; } 
    public string Title { get; set; } 
    public string Description { get; set; } 
    public string Url { get; set; }
    public string Tags { get; set; } 
    public int UserId { get; set; } 
    public User User { get; set; } 
}