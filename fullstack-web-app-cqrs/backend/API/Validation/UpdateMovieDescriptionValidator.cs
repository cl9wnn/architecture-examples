using API.Models.Movies.Requests;
using FluentValidation;

namespace API.Validation;

public class UpdateMovieDescriptionValidator: AbstractValidator<UpdateMovieDescriptionRequest>
{
    public UpdateMovieDescriptionValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(512)
            .WithMessage("Description must not exceed 512 characters");
    }
}