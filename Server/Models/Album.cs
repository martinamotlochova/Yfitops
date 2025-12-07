using System;
using Yfitops.Shared;

namespace Yfitops.Server.Models;

public class Album
{
    public Guid Id {get; set; }
    public string Name {get; set; }
    public DateTime ReleaseDate {get; set; }
    public ApplicationUser User {get; set; }

    public List<Track> Tracks {get; set;} = new List<Track>();

    public static Album ToEntity(AlbumContract contract)
    {
        return new Album()
        {
            Id = contract.Id,
            Name = contract.Name,
            ReleaseDate = contract.ReleaseDate
        };
    }

    public static AlbumContract ToContract(Album album)
    {
        return new AlbumContract()
        {
            Id = album.Id,
            Name = album.Name,
            ReleaseDate = album.ReleaseDate
        };
    }
}
