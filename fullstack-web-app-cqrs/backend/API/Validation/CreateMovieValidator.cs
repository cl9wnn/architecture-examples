using API.Models.Movies.Requests;
using Application.Features.Movies.Commands.Create;
using FluentValidation;

namespace API.Validation;

public class CreateMovieValidator: AbstractValidator<CreateMovieRequest>
{
    public CreateMovieValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(128)
            .WithMessage("Title must not exceed 64 characters");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(512)
            .WithMessage("Description must not exceed 512 characters");
    }
}