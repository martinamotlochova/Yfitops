using System;
using Yfitops.Shared;

namespace Yfitops.Server.Models;

public class Track : IEntityMapper<Track, TrackContract>
{
    public Guid Id {get; set; }
    public string Name {get; set; }
    public TimeSpan Duration {get; set; }

    public Guid AlbumId { get; set; }
    public Album Album {get; set; }

    public Guid? StorageId { get; set; }
    public Storage? Storage { get; set; }
    
    public List<ApplicationUser> UserFavorites {get; set; } = new List<ApplicationUser>();

    public static Track ToEntity(TrackContract contract)
    {
        return new Track()
        {
            Id = contract.Id,
            Name = contract.Name,
            Duration = contract.Duration,
            AlbumId = contract.AlbumId,
            StorageId = contract.StorageId
        };
    }

 public static TrackContract ToContract (Track track, string currentUserId)
    {
        return new TrackContract
        {
            Id = track.Id,
            AlbumId = track.AlbumId,
            Name = track.Name,
            Duration = track.Duration,
            IsFavourite = track.UserFavorites.Any(u => u.Id == currentUserId),
            StorageId = track.StorageId,
            Storage = track.Storage != null
            ? new StorageContract
            {
                Id = track.Storage.Id,
                FileName = track.Storage.FileName,
                Size = track.Storage.Size,
                Data = track.Storage.Data
            }
            : null
        };
    }
}
