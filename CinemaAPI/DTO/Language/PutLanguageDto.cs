using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Language;

public class PutLanguageDto
{
    [Required] public string? Label { get; set; }
}