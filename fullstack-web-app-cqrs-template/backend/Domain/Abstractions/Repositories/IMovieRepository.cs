using Application.Utils;
using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IMovieRepository
{
      Task<Result<Movie>> GetByIdAsync(int id);
      Task<Result<Movie>> AddAsync(Movie movie);
      Task<Result<Movie>> UpdateMovieDescriptionAsync(Movie movie);
      Task<Result> DeleteAsync(int id);
      Task<Result<ICollection<Movie>>> GetAllAsync();

}