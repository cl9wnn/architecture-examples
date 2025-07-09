using Application.Utils;
using MediatR;

namespace Application.Features.Movies.Commands.Create;

public record CreateMovieCommand(string Title, string Description) : IRequest<Result<int>>;
