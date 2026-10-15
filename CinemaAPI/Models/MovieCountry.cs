using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Models;

[PrimaryKey(nameof(MovieId), nameof(CountryId))]
public class MovieCountry
{
    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;

    public int CountryId { get; set; }

    public Country Country { get; set; } = null!;
}
