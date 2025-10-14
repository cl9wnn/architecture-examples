namespace API.GraphQL.Movies.Inputs;

public class CreateMovieInput
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<CreateActorInput>? Actors { get; set; }
}