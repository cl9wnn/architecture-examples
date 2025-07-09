namespace Domain.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public List<Actor> Actors { get; set; } = new();
}