using System;
using Yfitops.Shared;

namespace Yfitops.Server.Models;

public class Track
{
    public Guid Id {get; set; }
    public string Name {get; set; }
    public TimeSpan Duration {get; set; }

    public Guid AlbumId { get; set; }
    public Album Album {get; set; }
    
    public List<ApplicationUser> Users {get; set;} = new List<ApplicationUser>();

    public static Track ToEntity(TrackContract contract)
    {
        return new Track()
        {
            Id = contract.Id,
            Name = contract.Name,
            Duration = contract.Duration,
            AlbumId = contract.AlbumId
        };
    }

    public static TrackContract ToContract(Track track)
    {
        return new TrackContract()
        {
            Id = track.Id,
            Name = track.Name,
            Duration = track.Duration,
            AlbumId = track.AlbumId
        };
    }
}
