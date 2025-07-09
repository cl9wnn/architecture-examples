using Application.Utils;
using Domain.Entities;
using MediatR;

namespace Application.Features.Movies.Queries.Get;

public record GetMovieByIdQuery(int Id): IRequest<Result<Movie>>;