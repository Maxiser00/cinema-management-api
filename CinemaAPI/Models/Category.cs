using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Label { get; set; } = string.Empty;

    public List<MovieCategory> MovieCategories { get; set; } = new();
}
