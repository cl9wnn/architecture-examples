using Application.Utils;
using Domain.Abstractions.Repositories;
using MediatR;

namespace Application.Features.Movies.Commands.Delete;

public class DeleteMovieHandler(IMovieRepository movieRepository): IRequestHandler<DeleteMovieCommand, Result>
{
    public async Task<Result> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var deleteResult = await movieRepository.DeleteAsync(request.Id);
        
        return deleteResult.IsSuccess
            ? Result.Success()
            : Result.Failure(deleteResult.ErrorMessage!);
    }
}