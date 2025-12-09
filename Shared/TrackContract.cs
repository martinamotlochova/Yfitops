using System;

namespace Yfitops.Shared;

public class TrackContract
{
    public Guid Id {get; set; }
    public Guid AlbumId {get; set; }
    public string Name {get; set; }
    public TimeSpan Duration {get; set; }
}
