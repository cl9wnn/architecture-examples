using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class MovieRepository(AppDbContext dbContext, IMapper mapper) : IMovieRepository
{
    private IQueryable<MovieEntity> ActiveMovies => dbContext.Movies.Where(m => m.IsDeleted == false);
    
    public async Task<Movie?> GetByIdAsync(int id)
    {
        var movie = await ActiveMovies
            .Include(m => m.Actors)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        return mapper.Map<Movie?>(movie);
    }

    public async Task<Movie> AddAsync(Movie entity)
    {
        var movie = mapper.Map<MovieEntity>(entity);

        await dbContext.Movies.AddAsync(movie);
        await dbContext.SaveChangesAsync();

        return mapper.Map<Movie>(movie);
    }

    public async Task<Movie> UpdateAsync(Movie entity)
    {
        var existingMovie = await ActiveMovies
            .FirstOrDefaultAsync(m => m.Id == entity.Id);

        mapper.Map(entity, existingMovie);
        await dbContext.SaveChangesAsync();

        return mapper.Map<Movie>(existingMovie);
    }

    public async Task DeleteAsync(Movie entity)
    {
        var existingMovie = await ActiveMovies
            .FirstOrDefaultAsync(m => m.Id == entity.Id);

        existingMovie!.IsDeleted = true;
        await dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<Movie>> GetAllAsync()
    {
        var movies = await ActiveMovies
            .Include(m => m.Actors)
            .AsNoTracking()
            .ToListAsync();

        return mapper.Map<List<Movie>>(movies);
    }
}