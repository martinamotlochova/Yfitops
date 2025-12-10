using System;
using Microsoft.AspNetCore.SignalR;
using Yfitops.Shared;

namespace Yfitops.Server.Models;

public class Album
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime? ReleaseDate { get; set; }

    public Guid ArtistId { get; set; }
    public Artist Artist { get; set; }

    public List<ApplicationUser> UserFavorites { get; set; } = new List<ApplicationUser>();
    public List<Track> Tracks { get; set; } = new List<Track>();

    public static Album ToEntity(AlbumContract contract)
    {
        return new Album()
        {
            Id = contract.Id,
            Name = contract.Name,
            ReleaseDate = contract.ReleaseDate.Value,
            ArtistId = contract.ArtistId
        };
    }

    public static AlbumContract ToContract(Album album, string currentUserId)
    {
        return new AlbumContract
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            Name = album.Name,
            ReleaseDate = album.ReleaseDate,
            IsFavourite = album.UserFavorites.Any(u => u.Id == currentUserId)
        };
    }


}
