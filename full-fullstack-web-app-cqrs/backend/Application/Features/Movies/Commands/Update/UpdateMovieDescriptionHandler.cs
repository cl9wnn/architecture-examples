using Application.Utils;
using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Movies.Commands.Update;

public class UpdateMovieDescriptionHandler(IMovieRepository movieRepository, IMapper mapper): IRequestHandler<UpdateMovieDescriptionCommand, Result>
{
    public async Task<Result> Handle(UpdateMovieDescriptionCommand request, CancellationToken cancellationToken)
    {
        var movie = mapper.Map<Movie>(request);
        var updateResult = await movieRepository.UpdateMovieDescriptionAsync(movie);
        
        return updateResult.IsSuccess
            ? Result.Success()
            : Result.Failure(updateResult.ErrorMessage!);
    }
}