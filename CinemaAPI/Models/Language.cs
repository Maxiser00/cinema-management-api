using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.Models;

public class Language
{
    [Key] public int Id { get; set; }
    [MaxLength(255), Required] public string? Label { get; set; }
    
    public List<Movie>? Movies { get; set; }
}