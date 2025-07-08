namespace Infrastructure.Database.Entities;

public class MovieEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    
    public ICollection<ActorEntity> Actors { get; set; }
}