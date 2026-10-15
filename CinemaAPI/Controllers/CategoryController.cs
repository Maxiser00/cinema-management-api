using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaAPI.DTO.Category;
using CinemaAPI.DTO.Director;
using CinemaAPI.DTO.Movie;
using CinemaAPI.Models;

namespace CinemaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CinemaContext _context;

    public CategoryController(CinemaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        return Ok(await _context.Categories
            .Select(x => new GetLightCategoryDto()
            {
                Id = x.Id,
                Label = x.Label,
            })
            .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetCategoryDto>> GetCategoryById(int id)
    {
        var category = await _context.Categories
            .Where(x => x.Id == id)
            .Select(x => new GetCategoryDto
            {
                Id = x.Id,
                Label = x.Label,

                Movies = x.MovieCategories
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

                        Categories = mc.Movie.MovieCategories
                            .Select(c => c.Category.Label)
                            .ToList(),

                        Countries = mc.Movie.MovieCountries
                            .Select(c => c.Country.Label)
                            .ToList()
                    })
                    .ToList(),

                Directors = x.MovieCategories
                    .SelectMany(mc => mc.Movie.MovieDirectors)
                    .Select(md => new GetLightDirectorDto
                    {
                        Id = md.Director.Id,
                        Name = md.Director.Name,
                        BirthDate = md.Director.BirthDate,
                        DeathDate = md.Director.DeathDate,
                        Biography = md.Director.Biography,
                        Country = md.Director.Country.Label
                    })
                    .Distinct()
                    .ToList()
            })
            .FirstOrDefaultAsync();

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, PutCategoryDto putCategoryDto)
    {
        Category? category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        category.Label = putCategoryDto.Label;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<GetLightCategoryDto>> CreateCategory(
         PostCategoryDto postCategoryDto)
    {
        var category = new Category
        {
            Label = postCategoryDto.Label
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCategoryById),
            new { id = category.Id },
            new GetLightCategoryDto
            {
                Id = category.Id,
                Label = category.Label
            });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        Category? category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
