using Microsoft.AspNetCore.Identity;
using Yfitops.Shared;

namespace Yfitops.Server.Models;

public class ApplicationUser : IdentityUser
{
        public List<Album> AlbumFavourites {get; set; } = new List<Album>();
        public List<Artist> ArtistFavourites {get; set; } = new List<Artist>();
        public List<Track> TrackFavourites {get; set; } = new List<Track>();
        
}
