using API.GraphQL.Movies.Inputs;
using AutoMapper;
using Domain.Models;

namespace API.Mappings;

public class ApiMappingProfile: Profile
{
    public ApiMappingProfile()
    {
        CreateMap<CreateMovieInput, Movie>();
        CreateMap<CreateActorInput, Actor>();
    }
}