using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Models;

[PrimaryKey(nameof(MovieId), nameof(CategoryId))]
public class MovieCategory
{
    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;
}
