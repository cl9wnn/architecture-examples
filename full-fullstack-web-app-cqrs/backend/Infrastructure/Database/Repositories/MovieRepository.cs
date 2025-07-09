using Application.Utils;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class MovieRepository(AppDbContext dbContext) : IMovieRepository
{
    private IQueryable<Movie> ActiveMovies => dbContext.Movies.Where(m => m.IsDeleted == false);
    
    public async Task<Result<Movie>> GetByIdAsync(int id)
    {
        var movie = await ActiveMovies
            .Include(m => m.Actors)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return Result<Movie>.Failure("Movie not found!")!;
        }
        
        return Result<Movie>.Success(movie);
    }

    public async Task<Result<Movie>> AddAsync(Movie movie)
    {
        await dbContext.Movies.AddAsync(movie);
        await dbContext.SaveChangesAsync();

        return Result<Movie>.Success(movie);
    }

    public async Task<Result<Movie>> UpdateMovieDescriptionAsync(Movie movie)
    {
        var existingMovie = await ActiveMovies
            .FirstOrDefaultAsync(a => a.Id == movie.Id);

        if (existingMovie == null)
        {
            return Result<Movie>.Failure("Movie not found")!;
        }

        existingMovie.Description = movie.Description;
        await dbContext.SaveChangesAsync();
        
        return Result<Movie>.Success(existingMovie);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var existingMovie = await ActiveMovies
            .FirstOrDefaultAsync(a => a.Id == id);
        
        if (existingMovie == null)
        {
            return Result.Failure("Movie not found")!;
        }
        
        existingMovie!.IsDeleted = true;
        await dbContext.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<ICollection<Movie>>> GetAllAsync()
    {
        var movies = await ActiveMovies
            .Include(m => m.Actors)
            .AsNoTracking()
            .ToListAsync();
        
        return Result<ICollection<Movie>>.Success(movies);
    }
}