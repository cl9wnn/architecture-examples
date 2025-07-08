using Application.Utils;
using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Movies.Commands.Create;

public class CreateMovieHandler(IMovieRepository movieRepository, IMapper mapper): IRequestHandler<CreateMovieCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = mapper.Map<Movie>(request);
        
        var createResult = await movieRepository.AddAsync(movie);
        
        var createdMovie = createResult.Data;
        
        return createResult.IsSuccess
            ? Result<int>.Success(createdMovie.Id)
            : Result<int>.Failure(createResult.ErrorMessage!);
    }
}