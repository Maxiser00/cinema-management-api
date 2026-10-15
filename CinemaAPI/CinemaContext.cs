using CinemaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI;

public class CinemaContext(DbContextOptions<CinemaContext> options) : DbContext(options)
{
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Studio> Studios { get; set; }
    public DbSet<MovieCountry> CountryMovies { get; set; }
    public DbSet<MovieCategory> CategoryMovies { get; set; }
    public DbSet<MovieActor> ActorMovies { get; set; }
    public DbSet<MovieDirector> DirectorMovies { get; set; }
}
