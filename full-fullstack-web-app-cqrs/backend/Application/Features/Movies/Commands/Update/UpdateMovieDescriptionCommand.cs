using Application.Utils;
using MediatR;

namespace Application.Features.Movies.Commands.Update;

public record UpdateMovieDescriptionCommand(int Id, string Description): IRequest<Result>;