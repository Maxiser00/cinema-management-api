using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.Models;

public class Country
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Label { get; set; } = string.Empty;

    public List<Person> Persons { get; set; } = new();

    public List<MovieCountry> MovieCountries { get; set; } = new();
}
