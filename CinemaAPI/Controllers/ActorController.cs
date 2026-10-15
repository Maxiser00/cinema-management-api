using CinemaAPI.DTO.Actor;
using CinemaAPI.DTO.Movie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CinemaAPI.Models;

namespace CinemaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ActorController : ControllerBase
{
    private readonly CinemaContext _context;

    public ActorController(CinemaContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetActors()
    {
        return Ok(await _context.Actors
            .Include(x => x.Country)    
            .Select( x=> new GetLightActorDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    DeathDate = x.DeathDate,
                    Biography = x.Biography,
                    Country = x.Country.Label,
                })
            .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActorById(int id)
    {
        Actor? actor = await _context.Actors.FindAsync(id);

        if (actor != null)
        {
            return Ok(_context.Actors
                .Include(x => x.Country)
                .Where(x => x.Id == id)
                .Select(x => new GetActorDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    DeathDate = x.DeathDate,
                    Biography = x.Biography,
                    Country = x.Country.Label,
                    Movies = x.MovieActors
                        .Select(ma => new GetLightMovieDto {
                            Id = ma.Movie.Id,
                            Title = ma.Movie.Title,
                            Description = ma.Movie.Description,
                            ReleaseDate = ma.Movie.ReleaseDate,
                            Duration = ma.Movie.Duration,
                            Budget = ma.Movie.Budget,
                            Studio = ma.Movie.Studio.Label,
                            Language = ma.Movie.Language.Label,
                            Directors = ma.Movie.MovieDirectors
                                .Select(d => d.Director.Name)
                                .ToList(),
                            Categories = ma.Movie.MovieCategories
                                .Select(c => c.Category.Label)
                                .ToList(),
                            Countries = ma.Movie.MovieCountries
                                .Select(c => c.Country.Label)
                                .ToList(),
                        })
                        .ToList(),
                }));
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActor(int id, PutActorDto putActorDto)
    {
        Actor? actor = await _context.Actors.FindAsync(id);

        if (actor == null)
        {
            return NotFound();
        }
        
        actor.Name = putActorDto.Name;
        actor.BirthDate = putActorDto.BirthDate;
        actor.DeathDate = putActorDto.DeathDate;
        actor.Biography = putActorDto.Biography;
        actor.CountryId = putActorDto.CountryId;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateActor(PostActorDto postActorDto)
    {
        Actor actor = new()
        {
            Name = postActorDto.Name,
            BirthDate = postActorDto.BirthDate,
            DeathDate = postActorDto.DeathDate,
            Biography = postActorDto.Biography,
            CountryId = postActorDto.CountryId,
        };
        
        _context.Actors.Add(actor);
        await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        return Created();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActor(int id)
    {
        Actor? actor = await _context.Actors.FindAsync(id);
        if (actor == null)
        {
            return NotFound();
        }

        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}