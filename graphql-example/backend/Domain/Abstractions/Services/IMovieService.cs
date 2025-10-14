using Domain.Models;

namespace Domain.Abstractions.Services;

public interface IMovieService
{
    Task<Movie?> GetByIdAsync(int movieId);
    Task<List<Movie>> GetAllAsync();
    Task<Movie> CreateAsync(Movie movieDto);
    Task<bool> DeleteAsync(int movieId);
}