namespace CinemaAPI.Models;

public class Director : Person
{
    public List<MovieDirector> MovieDirectors { get; set; } = new();
}
