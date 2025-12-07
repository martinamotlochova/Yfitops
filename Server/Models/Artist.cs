using System;
using System.Runtime.CompilerServices;

namespace Yfitops.Server.Models;

public class Artist
{
    public Guid Id {get; set; }
    public string Name {get; set; }
    public ApplicationUser User {get; set; }

    public List<Album> Albums {get; set;} = new List<Album>();

}
