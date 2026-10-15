using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaAPI.DTO.Country;
using CinemaAPI.DTO.Director;
using CinemaAPI.DTO.Movie;
using CinemaAPI.Models;

namespace CinemaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CountryController : ControllerBase
{
    private readonly CinemaContext _context;

    public CountryController(CinemaContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCountries()
    {
        return Ok(await _context.Countries
            .Select( x=> new GetLightCountryDto()
                {
                    Id = x.Id,
                    Label = x.Label,
                })
            .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountryDto>> GetCountryById(int id)
    {
        Country? country = await _context.Countries.FindAsync(id);

        if (country != null)
        {
            return Ok(_context.Countries
                .Select(x => new GetCountryDto()
                {
                    Id = x.Id,
                    Label = x.Label,
                    Movies = x.MovieCountries
                        .Select(mc => new GetLightMovieDto
                        {
                            Id = mc.Movie.Id,
                            Title = mc.Movie.Title,
                            Description = mc.Movie.Description,
                            ReleaseDate = mc.Movie.ReleaseDate,
                            Duration = mc.Movie.Duration,
                            Budget = mc.Movie.Budget,
                            Studio = mc.Movie.Studio.Label,
                            Language = mc.Movie.Language.Label,
                            Directors = mc.Movie.MovieDirectors
                                .Select(d => d.Director.Name)
                                .ToList(),
                            Categories = mc.Movie.MovieCountries
                                .Select(c => c.Country.Label)
                                .ToList(),
                            Countries = mc.Movie.MovieCountries
                                .Select(c => c.Country.Label)
                                .ToList(),
                        })
                        .ToList(),
                }));
        }

        return NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCountry(int id, PutCountryDto putCountryDto)
    {
        Country? country = await _context.Countries.FindAsync(id);

        if (country == null)
        {
            return NotFound();
        }
        
        country.Label = putCountryDto.Label;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCountry(PostCountryDto postCountryDto)
    {
        Country country = new()
        {
            Label = postCountryDto.Label,
        };
        
        _context.Countries.Add(country);
        await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        return Created();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        Country? country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}