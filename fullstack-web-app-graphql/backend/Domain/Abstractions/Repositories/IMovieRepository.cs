using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IMovieRepository
{
      Task<Movie?> GetByIdAsync(int id);
      Task<Movie> AddAsync(Movie entity);
      Task<Movie> UpdateAsync(Movie entity);
      Task DeleteAsync(Movie entity);
      Task<ICollection<Movie>> GetAllAsync();

}