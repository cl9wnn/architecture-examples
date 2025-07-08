using Application.Utils;
using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IMovieRepository
{
      Task<Result<Movie>> GetByIdAsync(int id);
      Task<Result<Movie>> AddAsync(Movie movieDto);
      Task<Result<Movie>> UpdateAsync(Movie movieDto);
      Task<Result> DeleteAsync(int id);
      Task<Result<ICollection<Movie>>> GetAllAsync();

}