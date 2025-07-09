namespace Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    
    public ICollection<Actor> Actors { get; set; }
}