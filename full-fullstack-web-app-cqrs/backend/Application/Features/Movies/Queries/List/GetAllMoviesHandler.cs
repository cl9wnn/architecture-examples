using Application.Utils;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Movies.Queries.List;

public class GetAllMoviesHandler(IMovieRepository movieRepository): IRequestHandler<GetAllMoviesQuery, Result<List<Movie>>>
{
    public async Task<Result<List<Movie>>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
    {
        var getResult = await movieRepository.GetAllAsync();
        
        return getResult.IsSuccess
            ? Result<List<Movie>>.Success(getResult.Data.ToList())
            : Result<List<Movie>>.Failure(getResult.ErrorMessage!)!;    
    }
}