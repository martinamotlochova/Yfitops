using Microsoft.AspNetCore.Identity;

namespace Yfitops.Server.Models;

public class ApplicationUser : IdentityUser
{
    public List<Artist> FavoriteArtists {get; set; } = new List<Artist>();
    public List<Album> FavoriteAlbums {get; set; } = new List<Album>();
    public List<Track> FavoriteTracks {get; set; } = new List<Track>();
}
