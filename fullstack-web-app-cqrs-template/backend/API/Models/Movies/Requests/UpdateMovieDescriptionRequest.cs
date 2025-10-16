namespace API.Models.Movies.Requests;

public record UpdateMovieDescriptionRequest(int Id, string Description);