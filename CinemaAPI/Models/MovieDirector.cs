using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Models;

[PrimaryKey(nameof(MovieId), nameof(DirectorId))]
public class MovieDirector
{
    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;

    public int DirectorId { get; set; }

    public Director Director { get; set; } = null!;
}
