using Microsoft.AspNetCore.Identity;
using Yfitops.Shared;

namespace Yfitops.Server.Models;

public class ApplicationUser : IdentityUser
{
    public List<Artist> FavoriteArtists {get; set; } = new List<Artist>();
    public List<Album> FavoriteAlbums {get; set; } = new List<Album>();
    public List<Track> FavoriteTracks {get; set; } = new List<Track>();

    public static ApplicationUser ToEntity(UserContract contract)
    {
        return new ApplicationUser()
        {
            Id = contract.Id,
            UserName = contract.UserName,
            Email = contract.Email,

        };
    }

    public static UserContract ToContract(ApplicationUser user)
    {
        return new UserContract()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
        };
    }
}
