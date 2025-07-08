using API.Models.Movies.Requests;
using API.Models.Movies.Responses;
using Application.Features.Movies.Commands.Create;
using Application.Features.Movies.Commands.Update;
using AutoMapper;
using Domain.Models;

namespace API.Mappings;

public class ApiMappingProfile: Profile
{
    public ApiMappingProfile()
    {
        CreateMap<CreateMovieRequest, CreateMovieCommand>();
        CreateMap<Movie, MovieResponse>();
        CreateMap<CreateMovieCommand, Movie>();
        CreateMap<UpdateMovieDescriptionRequest, UpdateMovieDescriptionCommand>();
        CreateMap<UpdateMovieDescriptionCommand, Movie>();
    }
}