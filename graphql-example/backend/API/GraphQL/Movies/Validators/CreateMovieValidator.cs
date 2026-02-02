using API.GraphQL.Movies.Models;
using FluentValidation;

namespace API.GraphQL.Movies.Validators;

public class CreateMovieValidator: AbstractValidator<CreateMovieInput>
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