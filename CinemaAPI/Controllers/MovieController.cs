using CinemaAPI.DTO.Actor;
using CinemaAPI.DTO.Category;
using CinemaAPI.DTO.Director;
using CinemaAPI.DTO.Movie;
using CinemaAPI.DTO.Studio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaAPI.Models;

namespace CinemaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly CinemaContext _context;

    public MovieController(CinemaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        return Ok(await _context.Movies
            .Include(x => x.MovieDirectors)
            .Include(x => x.MovieCategories)
            .Include(x => x.MovieCountries)
            .Select(x => new GetLightMovieDto()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ReleaseDate = x.ReleaseDate,
                Duration = x.Duration,
                Budget = x.Budget,
                Studio = x.Studio.Label,
                Language = x.Language.Label,
                Directors = x.MovieDirectors
                    .Select(x => x.Director.Name)
                    .ToList(),
                Categories = x.MovieCategories
                    .Select(x => x.Category.Label)
                    .ToList(),
                Countries = x.MovieCountries
                    .Select(x => x.Country.Label)
                    .ToList(),
            })
            .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        Movie? movie = await _context.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }
        
        var movieDto = await _context.Movies
            .Where(x => x.Id == id)
            .Include(x => x.MovieDirectors!)
            .ThenInclude(md => md.Director)
            .Include(x => x.MovieCategories!)
            .ThenInclude(mc => mc.Category)
            .Include(x => x.MovieCountries!)
            .ThenInclude(mc => mc.Country)
            .Include(x => x.Studio)
            .Select(x => new GetMovieDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ReleaseDate = x.ReleaseDate,
                Duration = x.Duration,
                Budget = x.Budget,
                Studio = new GetLightStudioDto
                {
                    Id = x.Studio.Id,
                    Label = x.Studio.Label
                },
                Language = x.Language.Label,
                Actors = x.MovieActors
                    .Select(ma => new GetLightActorDto
                    {
                        Id = ma.Actor.Id,
                        Name = ma.Actor.Name,
                        BirthDate = ma.Actor.BirthDate,
                        DeathDate = ma.Actor.DeathDate,
                        Biography = ma.Actor.Biography,
                        Country = ma.Actor.Country.Label,
                    })
                    .ToList(),
                Directors = x.MovieDirectors
                    .Select(md => new GetLightDirectorDto
                    {
                        Id = md.Director.Id,
                        Name = md.Director.Name,
                        BirthDate = md.Director.BirthDate,
                        DeathDate = md.Director.DeathDate,
                        Biography = md.Director.Biography,
                        Country = md.Director.Country.Label,
                    })
                    .ToList(),
                Categories = x.MovieCategories
                    .Select(mc => new GetLightCategoryDto
                    {
                        Id = mc.Category.Id,
                        Label = mc.Category.Label
                    })
                    .ToList(),
                Countries = x.MovieCountries
                    .Select(mc => mc.Country.Label)
                    .ToList()
            })
            .FirstOrDefaultAsync();

        return Ok(movieDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie(PostMovieDto postMovieDto)
    {
        Movie movie = new()
        {
            Title = postMovieDto.Title,
            Description = postMovieDto.Description,
            ReleaseDate = postMovieDto.ReleaseDate,
            Duration = postMovieDto.Duration,
            Budget = postMovieDto.Budget,
            StudioId = postMovieDto.StudioId,
            LanguageId = postMovieDto.LanguageId,
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        return Created();
    }

    [HttpPost("{id}/actors")]
    public async Task<IActionResult> PostActorById(int id, PostMovieActorDto postMovieActorDto)
    {
        Movie? movie = await _context.Movies.Include(x => x.MovieActors).FirstOrDefaultAsync(x => x.Id == id);

        if (movie == null) return NotFound();
        
        Actor? actor = await _context.Actors.FindAsync(postMovieActorDto.ActorId);

        if (actor == null) return NotFound();
        
        movie.MovieActors.Add(new MovieActor{MovieId = id, ActorId = actor.Id});
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpPost("{id}/directors")]
    public async Task<IActionResult> PostDirectorById(int id, PostMovieDirectorDto postMovieDirectorDto)
    {
        Movie? movie = await _context.Movies.Include(x => x.MovieDirectors).FirstOrDefaultAsync(x => x.Id == id);

        if (movie == null) return NotFound();
        
        Director? director = await _context.Directors.FindAsync(postMovieDirectorDto.DirectorId);

        if (director == null) return NotFound();
        
        movie.MovieDirectors.Add(new MovieDirector{MovieId = id, DirectorId = director.Id});
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpPost("{id}/categories")]
    public async Task<IActionResult> PostCategoryById(int id, PostMovieCategoryDto postMovieCategoryDto)
    {
        Movie? movie = await _context.Movies.Include(x => x.MovieCategories).FirstOrDefaultAsync(x => x.Id == id);

        if (movie == null) return NotFound();
        
        Category? category = await _context.Categories.FindAsync(postMovieCategoryDto.CategoryId);

        if (category == null) return NotFound();
        
        movie.MovieCategories.Add(new MovieCategory{MovieId = id, CategoryId = category.Id});
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpPost("{id}/countries")]
    public async Task<IActionResult> PostCountryById(int id, PostMovieCountryDto postMovieCountryDto)
    {
        Movie? movie = await _context.Movies.Include(x => x.MovieCountries).FirstOrDefaultAsync(x => x.Id == id);

        if (movie == null) return NotFound();
        
        Country? country = await _context.Countries.FindAsync(postMovieCountryDto.CountryId);

        if (country == null) return NotFound();
        
        movie.MovieCountries.Add(new MovieCountry{MovieId = id, CountryId = country.Id});
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, PutMovieDto putMovieDto)
    {
        Movie? movie = await _context.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        movie.Title = putMovieDto.Title;
        movie.Description = putMovieDto.Description;
        movie.ReleaseDate = putMovieDto.ReleaseDate;
        movie.Duration = putMovieDto.Duration;
        movie.Budget = putMovieDto.Budget;
        movie.LanguageId = putMovieDto.LanguageId;
        movie.StudioId = putMovieDto.StudioId;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpPut("{id}/actor/{actorId}")]
    public async Task<IActionResult> UpdateActorInMovie(int id, int actorId, PostMovieActorDto postMovieActorDto)
    {
        MovieActor? movieActor = await _context.ActorMovies.FirstOrDefaultAsync(x => x.MovieId == id && x.ActorId == actorId);
        if (movieActor == null) return NotFound();

        IActionResult removeResult = await RemoveActorFromMovie(id, actorId);
        IActionResult AddResult = await PostActorById(id, postMovieActorDto);
        
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpPut("{id}/director/{directorId}")]
    public async Task<IActionResult> UpdateDirectorInMovie(int id, int directorId, PostMovieDirectorDto postMovieDirectorDto)
    {
        MovieDirector? movieDirector = await _context.DirectorMovies.FirstOrDefaultAsync(x => x.MovieId == id && x.DirectorId == directorId);
        if (movieDirector == null) return NotFound();

        IActionResult removeResult = await RemoveDirectorFromMovie(id, directorId); ;
        IActionResult addResult = await PostDirectorById(id, postMovieDirectorDto);
        
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpPut("{id}/category/{categoryId}")]
    public async Task<IActionResult> UpdateCategoryInMovie(int id, int categoryId, PostMovieCategoryDto postMovieCategoryDto)
    {
        MovieCategory? movieCategory = await _context.CategoryMovies.FirstOrDefaultAsync(x => x.MovieId == id && x.CategoryId == categoryId);
        if (movieCategory == null) return NotFound();

        IActionResult removeResult = await RemoveCategoryFromMovie(id, categoryId); ;
        IActionResult addResult = await PostCategoryById(id, postMovieCategoryDto);
        
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpPut("{id}/country/{countryId}")]
    public async Task<IActionResult> UpdateCcountryInMovie(int id, int countryId, PostMovieCountryDto postMovieCountryDto)
    {
        MovieCountry? movieCountry = await _context.CountryMovies.FirstOrDefaultAsync(x => x.MovieId == id && x.CountryId == countryId);
        if (movieCountry == null) return NotFound();

        IActionResult removeResult = await RemoveCountryFromMovie(id, countryId); ;
        IActionResult addResult = await PostCountryById(id, postMovieCountryDto);
        
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        Movie? movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("{id}/actor/{actorId}")]
    public async Task<IActionResult> RemoveActorFromMovie(int id, int actorId)
    {
        MovieActor? movieActor = await _context.ActorMovies.FirstOrDefaultAsync(x => x.MovieId == id && x.ActorId == actorId);
        if (movieActor == null) return NotFound();

        _context.ActorMovies.Remove(movieActor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("{id}/director/{directorId}")]
    public async Task<IActionResult> RemoveDirectorFromMovie(int id, int directorId)
    {
        MovieDirector? movieDirector = await _context.DirectorMovies.FirstOrDefaultAsync(x => x.MovieId == id && x.DirectorId == directorId);
        if (movieDirector == null) return NotFound();

        _context.DirectorMovies.Remove(movieDirector);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("{id}/category/{categoryId}")]
    public async Task<IActionResult> RemoveCategoryFromMovie(int id, int categoryId)
    {
        MovieCategory? movieCategory = await _context.CategoryMovies.FirstOrDefaultAsync(x => x.MovieId == id && x.CategoryId == categoryId);
        if (movieCategory == null) return NotFound();

        _context.CategoryMovies.Remove(movieCategory);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("{id}/country/{countryId}")]
    public async Task<IActionResult> RemoveCountryFromMovie(int id, int countryId)
    {
        MovieCountry? movieCountry = await _context.CountryMovies.FirstOrDefaultAsync(x => x.MovieId == id && x.CountryId == countryId);
        if (movieCountry == null) return NotFound();

        _context.CountryMovies.Remove(movieCountry);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}