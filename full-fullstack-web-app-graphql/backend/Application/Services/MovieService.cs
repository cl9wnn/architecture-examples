using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;

namespace Application.Services;

public class MovieService(IMovieRepository movieRepository): IMovieService
{
    public async Task<Movie?> GetByIdAsync(int movieId)
    {
        return await movieRepository.GetByIdAsync(movieId);
    }

    public async Task<List<Movie>> GetAllAsync()
    {
        var movies = await movieRepository.GetAllAsync();
        return movies.ToList();
    }

    public async Task<Movie> CreateAsync(Movie movieDto)
    {
        return await movieRepository.AddAsync(movieDto);
    }
    
    public async Task<bool> DeleteAsync(int movieId)
    {
        var movie = await movieRepository.GetByIdAsync(movieId);
        
        if (movie == null)
            return false;
        
        await movieRepository.DeleteAsync(movie);
        return true;
    }
}