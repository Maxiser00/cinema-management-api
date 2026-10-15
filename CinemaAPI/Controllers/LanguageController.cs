using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaAPI.DTO.Language;
using CinemaAPI.Models;

namespace CinemaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class LanguageController : ControllerBase
{
    private readonly CinemaContext _context;

    public LanguageController(CinemaContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetLanguages()
    {
        return Ok(await _context.Languages
            .Select( x=> new GetLightLanguageDto()
                {
                    Id = x.Id,
                    Label = x.Label,
                })
            .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetLanguageDto>> GetLanguageById(int id)
    {
        Language? language = await _context.Languages.FindAsync(id);

        if (language != null)
        {
            return Ok(_context.Languages
                .Select(x => new GetLightLanguageDto()
                {
                    Id = x.Id,
                    Label = x.Label,
                }));
        }

        return NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLanguage(int id, PutLanguageDto putLanguageDto)
    {
        Language? language = await _context.Languages.FindAsync(id);

        if (language == null)
        {
            return NotFound();
        }
        
        language.Label = putLanguageDto.Label;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCountry(PostLanguageDto postLanguageDto)
    {
        Language language = new()
        {
            Label = postLanguageDto.Label,
        };
        
        _context.Languages.Add(language);
        await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        return Created();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLanguage(int id)
    {
        Language? language = await _context.Languages.FindAsync(id);
        if (language == null)
        {
            return NotFound();
        }

        _context.Languages.Remove(language);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}