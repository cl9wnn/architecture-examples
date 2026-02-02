using API.GraphQL.Movies.Models;
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