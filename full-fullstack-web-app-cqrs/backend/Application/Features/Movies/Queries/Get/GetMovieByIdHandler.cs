using Application.Utils;
using Domain.Abstractions.Repositories;
using Domain.Models;
using MediatR;

namespace Application.Features.Movies.Queries.Get;

public class GetMovieByIdHandler(IMovieRepository movieRepository): IRequestHandler<GetMovieByIdQuery, Result<Movie>>
{
    public async Task<Result<Movie>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var getResult = await movieRepository.GetByIdAsync(request.Id);
        
        return getResult.IsSuccess
            ? Result<Movie>.Success(getResult.Data)
            : Result<Movie>.Failure(getResult.ErrorMessage!)!;
    }
}