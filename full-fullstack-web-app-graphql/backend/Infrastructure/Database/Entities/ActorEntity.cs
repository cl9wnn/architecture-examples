namespace Infrastructure.Database.Entities;

public class ActorEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public ICollection<MovieEntity> Movies { get; set; }
}