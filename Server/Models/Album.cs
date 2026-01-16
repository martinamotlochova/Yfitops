using System;
using Microsoft.AspNetCore.SignalR;
using Yfitops.Shared;

namespace Yfitops.Server.Models;

public class Album : IEntityMapper<Album, AlbumContract>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime? ReleaseDate { get; set; }

    public Guid ArtistId { get; set; }
    public Artist Artist { get; set; }

    public Guid? CoverImageId { get; set; }
    public Storage? CoverImage { get; set; }

    public Guid? UserId { get; set; }

    public List<ApplicationUser> UserFavorites { get; set; } = new List<ApplicationUser>();
    public List<Track> Tracks { get; set; } = new List<Track>();

    public static Album ToEntity(AlbumContract contract)
    {
        return new Album()
        {
            Id = contract.Id,
            Name = contract.Name,
            ReleaseDate = contract.ReleaseDate.Value,
            ArtistId = contract.ArtistId,
            CoverImageId = contract.CoverImageId
            //CoverImage sa nastavi az pri ulozenom storage zazname
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
            IsFavourite = album.UserFavorites.Any(u => u.Id == currentUserId),
            CoverImageId = album.CoverImageId,
            CoverImage = album.CoverImage != null 
            ? new StorageContract
            {
                Id = album.CoverImage.Id,
                FileName = album.CoverImage.FileName,
                Size = album.CoverImage.Size,
                Data = album.CoverImage.Data
            }
            : null

        };
    }


}
