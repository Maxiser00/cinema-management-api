using CinemaAPI.DTO.Director;
using CinemaAPI.DTO.Movie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaAPI.DTO.Studio;
using CinemaAPI.Models;

namespace CinemaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class StudioController : ControllerBase
{
    private readonly CinemaContext _context;

    public StudioController(CinemaContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetStudios()
    {
        return Ok(await _context.Studios
            .Select( x=> new GetLightStudioDto()
                {
                    Id = x.Id,
                    Label = x.Label,
                })
            .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetStudioDto>> GetStudioById(int id)
    {
        Studio? studio = await _context.Studios.FindAsync(id);

        if (studio != null)
        {
            return Ok(_context.Studios
                .Select(x => new GetStudioDto()
                {
                    Id = x.Id,
                    Label = x.Label,
                    Movies = x.Movies   
                        .Where(Movies => Movies.StudioId == id)
                        .Select(m => new GetLightMovieDto
                        {
                            Id = m.Id,
                            Title = m.Title,
                            Description = m.Description,
                            ReleaseDate = m.ReleaseDate,
                            Duration = m.Duration,
                            Budget = m.Budget,
                            Studio = m.Studio.Label,
                            Language = m.Language.Label,
                            Directors = m.MovieDirectors
                                .Select(d => d.Director.Name)
                                .ToList(),
                            Categories = m.MovieCategories
                                .Select(c => c.Category.Label)
                                .ToList(),
                            Countries = m.MovieCountries
                                .Select(c => c.Country.Label)
                                .ToList(),
                        })
                        .ToList(), 
                    Directors = x.Movies.SelectMany(y => y.MovieDirectors)
                        .Select(md => new GetLightDirectorDto
                        {
                            Id = md.Director.Id,
                            Name = md.Director.Name,
                            BirthDate = md.Director.BirthDate,
                            DeathDate = md.Director.DeathDate,
                            Biography = md.Director.Biography,
                            Country = md.Director.Country.Label,
                        })
                        .Distinct()
                        .ToList()
                }));
        }

        return NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudio(int id, PutStudioDto putStudioDto)
    {
        Studio? studios = await _context.Studios.FindAsync(id);

        if (studios == null)
        {
            return NotFound();
        }
        
        studios.Label = putStudioDto.Label;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateStudio(PostStudioDto postStudiosDto)
    {
        Studio studio = new()
        {
            Label = postStudiosDto.Label,
        };
        
        _context.Studios.Add(studio);
        await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        return Created();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudios(int id)
    {
        Studio? studio = await _context.Studios.FindAsync(id);
        if (studio == null)
        {
            return NotFound();
        }

        _context.Studios.Remove(studio);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}