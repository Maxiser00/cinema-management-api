using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Models;

public class Actor:Person
{
    public List<MovieActor> MovieActors { get; set; } = new();
}
