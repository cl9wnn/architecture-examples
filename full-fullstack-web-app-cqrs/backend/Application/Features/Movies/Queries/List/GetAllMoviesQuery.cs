using Application.Utils;
using Domain.Entities;
using MediatR;

namespace Application.Features.Movies.Queries.List;

public record GetAllMoviesQuery: IRequest<Result<List<Movie>>>;