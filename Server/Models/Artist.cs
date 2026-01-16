using System;
using System.Runtime.CompilerServices;
using Yfitops.Shared;

namespace Yfitops.Server.Models;

public class Artist : IEntityMapper<Artist, ArtistContract>
{
    public Guid Id {get; set; }
    public string Name {get; set; }

    public Guid? UserId { get; set; }
    public List<ApplicationUser> UserFavorites {get; set; } = new List<ApplicationUser>();
    public List<Album> Albums {get; set;} = new List<Album>();

    public static Artist ToEntity(ArtistContract contract)
    {
        return new Artist()
        {
            Id = contract.Id,
            Name = contract.Name,
        };
    }

    public static ArtistContract ToContract(Artist artist, string currentUserId)
    {
        return new ArtistContract()
        {
            Id = artist.Id,
            Name = artist.Name,
            IsFavourite = artist.UserFavorites.Any(u => u.Id == currentUserId)
        };
    }

}
