using System;
using System.Runtime.CompilerServices;
using Yfitops.Shared;

namespace Yfitops.Server.Models;

public class Artist
{
    public Guid Id {get; set; }
    public string Name {get; set; }

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

    public static ArtistContract ToContract(Artist artist)
    {
        return new ArtistContract()
        {
            Id = artist.Id,
            Name = artist.Name,
        };
    }

}
