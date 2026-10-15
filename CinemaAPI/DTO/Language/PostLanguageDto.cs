using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Language;

public class PostLanguageDto
{
    [Required] public string? Label { get; set; }
}