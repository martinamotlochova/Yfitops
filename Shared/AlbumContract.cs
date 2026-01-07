using System;

namespace Yfitops.Shared;

public class AlbumContract
{
    public Guid Id {get; set; }
    public Guid ArtistId {get; set;}
    public string Name {get; set; }
    public DateTime? ReleaseDate {get; set; }
    public bool IsFavourite {get; set; }

    public StorageContract CoverImageId { get; set; }
    public StorageContract CoverImage { get; set; }

}
