using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Category;

public class PutCategoryDto
{
    [Required]
    public string Label { get; set; } = null!;
}
