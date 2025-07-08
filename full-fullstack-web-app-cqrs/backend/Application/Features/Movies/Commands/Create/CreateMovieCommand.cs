using Application.Utils;
using Domain.Models;
using MediatR;

namespace Application.Features.Movies.Commands.Create;

public record CreateMovieCommand(string Title, string Description) : IRequest<Result<int>>;
