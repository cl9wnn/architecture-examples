using API.Models.Movies.Requests;
using API.Models.Movies.Responses;
using Application.Features.Movies.Commands.Create;
using Application.Features.Movies.Commands.Delete;
using Application.Features.Movies.Commands.Update;
using Application.Features.Movies.Queries.Get;
using Application.Features.Movies.Queries.List;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(IMediator mediator, IMapper mapper): ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMovieAsync(int id)
    {
        var query = new GetMovieByIdQuery(id);
        var getResult = await mediator.Send(query);
        
        var movie = mapper.Map<MovieResponse>(getResult.Data);
        
        return getResult.IsSuccess
            ? Ok(movie) 
            : NotFound(getResult.ErrorMessage);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllMoviesAsync()
    {
        var query = new GetAllMoviesQuery();
        var getResult = await mediator.Send(query);

        var movies = mapper.Map<List<MovieResponse>>(getResult.Data);
        
        return Ok(movies); 
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovieAsync(CreateMovieRequest request)
    {
        var cmd = mapper.Map<CreateMovieCommand>(request);
        var createResult = await mediator.Send(cmd);
        
        return createResult.IsSuccess
            ? Ok(createResult.Data)
            : BadRequest(createResult.ErrorMessage);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateMovieDescriptionAsync(UpdateMovieDescriptionRequest request)
    {
        var cmd = mapper.Map<UpdateMovieDescriptionCommand>(request);
        var updateResult = await mediator.Send(cmd);
        
        return updateResult.IsSuccess
            ? Ok()
            : BadRequest(updateResult.ErrorMessage);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteMovieAsync(int id)
    {
        var cmd = new DeleteMovieCommand(id);
        var deleteResult = await mediator.Send(cmd);
        
        return deleteResult.IsSuccess
            ? Ok()
            : BadRequest(deleteResult.ErrorMessage);
    }
}