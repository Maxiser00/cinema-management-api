using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO.Category;

public class PostCategoryDto
{
    [Required]
    public string Label { get; set; } = null!;
}
