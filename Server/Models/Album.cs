using System;

namespace Yfitops.Server.Models;

public class Album
{
    public Guid Id {get; set; }
    public string Name {get; set; }
    public DateTime ReleaseDate {get; set; }
    public ApplicationUser User {get; set; }

    public List<Track> Tracks {get; set;} = new List<Track>();
}
