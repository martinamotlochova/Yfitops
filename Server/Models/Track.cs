using System;

namespace Yfitops.Server.Models;

public class Track
{
    public Guid Id {get; set; }
    public string Name {get; set; }
    public TimeSpan Duration {get; set; }
    public ApplicationUser User {get; set; }


}
