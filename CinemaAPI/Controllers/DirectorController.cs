using CinemaAPI.DTO.Director;
using CinemaAPI.DTO.Movie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaAPI.Models;

namespace CinemaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class DirectorController : ControllerBase
{
    private readonly CinemaContext _context;

    public DirectorController(CinemaContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetDirectors()
    {
        return Ok(await _context.Directors
            .Include(x => x.Country)    
            .Select( x=> new GetLightDirectorDto()
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
    public async Task<IActionResult> GetDirectorById(int id)
    {
        Director? director = await _context.Directors.FindAsync(id);

        if (director != null)
        {
            return Ok(_context.Directors
                .Include(x => x.Country)
                .Where(x => x.Id == id)
                .Select(x => new GetDirectorDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    DeathDate = x.DeathDate,
                    Biography = x.Biography,
                    Country = x.Country.Label,
                    Movies = x.MovieDirectors
                        .Select(ma => new GetLightMovieDto() {
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
    public async Task<IActionResult> UpdateDirector(int id, PutDirectorDto putDirectorDto)
    {
        Director? director = await _context.Directors.FindAsync(id);

        if (director == null)
        {
            return NotFound();
        }
        
        director.Name = putDirectorDto.Name;
        director.BirthDate = putDirectorDto.BirthDate;
        director.DeathDate = putDirectorDto.DeathDate;
        director.Biography = putDirectorDto.Biography;
        director.CountryId = putDirectorDto.CountryId;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateDirector(PostDirectorDto postDirectorDto)
    {
        Director director = new()
        {
            Name = postDirectorDto.Name,
            BirthDate = postDirectorDto.BirthDate,
            DeathDate = postDirectorDto.DeathDate,
            Biography = postDirectorDto.Biography,
            CountryId = postDirectorDto.CountryId,
        };
        
        _context.Directors.Add(director);
        await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        return Created();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDirector(int id)
    {
        Director? director = await _context.Directors.FindAsync(id);
        if (director == null)
        {
            return NotFound();
        }

        _context.Directors.Remove(director);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}