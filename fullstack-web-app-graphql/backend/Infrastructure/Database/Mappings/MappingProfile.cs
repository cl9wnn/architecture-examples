using AutoMapper;
using Domain.Models;
using Infrastructure.Database.Entities;

namespace Infrastructure.Database.Mappings;

public class InfrastructureMappingProfile: Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<Movie, MovieEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ReverseMap(); 
        
        CreateMap<Actor, ActorEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap();
    }
}