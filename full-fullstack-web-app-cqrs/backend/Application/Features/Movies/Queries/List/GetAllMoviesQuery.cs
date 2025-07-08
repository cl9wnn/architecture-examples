using Application.Utils;
using Domain.Models;
using MediatR;

namespace Application.Features.Movies.Queries.List;

public record GetAllMoviesQuery: IRequest<Result<List<Movie>>>;