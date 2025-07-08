using Application.Utils;
using Domain.Models;
using MediatR;

namespace Application.Features.Movies.Queries.Get;

public record GetMovieByIdQuery(int Id): IRequest<Result<Movie>>;