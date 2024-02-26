using System;
using System.Collections.Generic;

namespace Sakila.Domain.Model;

public partial class Actor : BaseEntity
{
    public int ActorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime LastUpdate { get; set; }

    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();
}
