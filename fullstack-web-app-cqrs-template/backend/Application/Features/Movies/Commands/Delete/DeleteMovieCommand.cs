using Application.Utils;
using MediatR;

namespace Application.Features.Movies.Commands.Delete;

public record DeleteMovieCommand(int Id): IRequest<Result>;