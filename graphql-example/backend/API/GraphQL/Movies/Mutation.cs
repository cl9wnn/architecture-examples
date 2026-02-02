using API.GraphQL.Movies.Models;
using AutoMapper;
using Domain.Abstractions.Services;
using Domain.Models;
using FluentValidation;

namespace API.GraphQL.Movies;

[MutationType]
public class Mutation
{
    public async Task<Movie> CreateMovie([Service] IMovieService service, [Service] IMapper mapper,
        CreateMovieInput input, [Service] IValidator<CreateMovieInput> validator)
    {
            var validationResult = await validator.ValidateAsync(input);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => ErrorBuilder.New()
                        .SetMessage(e.ErrorMessage)
                        .SetCode("VALIDATION_ERROR")
                        .Build())
                    .ToList();

                throw new GraphQLException(errors);
            }
            
            var movieDto = mapper.Map<Movie>(input);
            
            var newMovie = await service.CreateAsync(movieDto);

            return newMovie;
    }

    public async Task<bool> DeleteMovie([Service] IMovieService service, int id)
    {
        var isDeleted = await service.DeleteAsync(id);

        if (!isDeleted)
        {
            throw new GraphQLException(ErrorBuilder.New()
                .SetMessage("Movie not found!")
                .SetCode("NOT_FOUND")
                .Build());
        }
        return isDeleted;
    }
}