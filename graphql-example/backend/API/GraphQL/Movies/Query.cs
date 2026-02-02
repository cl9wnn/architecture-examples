using Domain.Abstractions.Services;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.GraphQL.Movies;

[QueryType]
public class Query
{
    public async Task<List<Movie>> GetAllMovies([Service] IMovieService movieService)
    {
        var movies = await movieService.GetAllAsync();

        if (movies.IsNullOrEmpty())
        {
            throw new GraphQLException(ErrorBuilder.New()
                .SetMessage("No movies found!")
                .SetCode("EMPTY_RESULT")
                .Build());
        }
        
        return movies;
    }

    public async Task<Movie> GetMovieById([Service] IMovieService movieService, int id)
    {
        var movie = await movieService.GetByIdAsync(id);

        if (movie == null)
        {
            throw new GraphQLException(ErrorBuilder.New()
                .SetMessage("Movie not found!")
                .SetCode("NOT_FOUND")
                .Build());
        }
        
        return movie;
    }
}