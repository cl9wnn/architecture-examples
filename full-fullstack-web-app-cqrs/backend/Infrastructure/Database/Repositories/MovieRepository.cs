using Application.Utils;
using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class MovieRepository(AppDbContext dbContext, IMapper mapper) : IMovieRepository
{
    private IQueryable<MovieEntity> ActiveMovies => dbContext.Movies.Where(m => m.IsDeleted == false);
    
    public async Task<Result<Movie>> GetByIdAsync(int id)
    {
        var movieEntity = await ActiveMovies
            .Include(m => m.Actors)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movieEntity == null)
        {
            return Result<Movie>.Failure("Movie not found!")!;
        }
        
        var movie =  mapper.Map<Movie>(movieEntity);
        return Result<Movie>.Success(movie);
    }

    public async Task<Result<Movie>> AddAsync(Movie movieDto)
    {
        var movieEntity = mapper.Map<MovieEntity>(movieDto);

        await dbContext.Movies.AddAsync(movieEntity);
        await dbContext.SaveChangesAsync();

        var newMovie = mapper.Map<Movie>(movieEntity);
        return Result<Movie>.Success(newMovie);
    }

    public async Task<Result<Movie>> UpdateAsync(Movie movieDto)
    {
        var existingMovie = await ActiveMovies
            .FirstOrDefaultAsync(a => a.Id == movieDto.Id);

        if (existingMovie == null)
        {
            return Result<Movie>.Failure("Movie not found")!;
        }

        mapper.Map(movieDto, existingMovie);
        await dbContext.SaveChangesAsync();
        
        var updated = mapper.Map<Movie>(existingMovie);
        return Result<Movie>.Success(updated);
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
        var movieEntities = await ActiveMovies
            .Include(m => m.Actors)
            .AsNoTracking()
            .ToListAsync();

        var movies = mapper.Map<ICollection<Movie>>(movieEntities);
        
        return Result<ICollection<Movie>>.Success(movies);
    }
}