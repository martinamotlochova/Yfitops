using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using Yfitops.Server.Data;
using Yfitops.Server.Models;
using Yfitops.Shared;

namespace Yfitops.Server.Services
{
    public class AlbumService
    {
        private ApplicationDbContext context;

        public AlbumService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<AlbumContract> GetAlbumByIdAsync(Guid id, string currentUserId)
        {
            var album = await context.Albums.FindAsync(id);
            if (album == null)
            {
                return null;
            }
            return Album.ToContract(album, currentUserId);
        }

        public async Task<List<AlbumContract>> GetArtistAlbumsAsync(Guid artistId, string currentUserId)
        {
            var artist = await context.Artists.Include(a => a.Albums).ThenInclude(a => a.CoverImage).Include(a => a.Albums).ThenInclude(a => a.UserFavorites).FirstOrDefaultAsync(a => a.Id == artistId);


            if (artist == null)
            {
                return new List<AlbumContract>();
            }

            return artist.Albums.Select(a => Album.ToContract(a, currentUserId)).ToList();
        }

        public async Task<AlbumContract> CreateAlbumAsync(AlbumContract album, string currentUserId)
        {
            //vieme ze album bude mat obrazok, tym padom si album bude so sebou niest odkaz na obrazok, musime ale najskor mu vytvorit ze teda ten obrazok tu bude

            Storage storageEntity = null;

            if (album.CoverImage != null)
            {
                storageEntity = new Storage
                {
                    Id = Guid.NewGuid(),
                    FileName = album.CoverImage.FileName,
                    Data = album.CoverImage.Data,
                    Size = album.CoverImage.Size
                };

                context.Storages.Add(storageEntity);
            }
            Album entity = Album.ToEntity(album);

            if (storageEntity != null)
            {
                entity.CoverImageId = storageEntity.Id;
                entity.CoverImage = storageEntity;
            }

            context.Albums.Add(entity);

            var user = await context.Users.Include(u => u.AlbumFavourites).FirstOrDefaultAsync(u => u.Id == currentUserId);
            if (user != null)
            {
                if (album.IsFavourite)
                {
                    if (!user.AlbumFavourites.Contains(entity))
                        user.AlbumFavourites.Add(entity);
                }
                else
                {
                    if (user.AlbumFavourites.Contains(entity))
                        user.AlbumFavourites.Remove(entity);
                }
            }

            await context.SaveChangesAsync();

            return Album.ToContract(entity, currentUserId);
        }

        public async Task<AlbumContract> UpdateAlbumAsync(Guid id, AlbumContract contract, string currentUserId)
        {
            var album = await context.Albums.Include(a => a.CoverImage).FirstOrDefaultAsync(a => a.Id == id);

            if (album == null)
                return null;

            album.Name = contract.Name;
            album.ReleaseDate = contract.ReleaseDate;

            if (contract.CoverImage != null)
            {
                Storage storageEntity;

                if (album.CoverImage != null)
                {
                    storageEntity = album.CoverImage;
                    storageEntity.FileName = contract.CoverImage.FileName;
                    storageEntity.Data = contract.CoverImage.Data;
                    storageEntity.Size = contract.CoverImage.Size;
                }
                else
                {
                    
                    storageEntity = new Storage
                    {
                        Id = Guid.NewGuid(),
                        FileName = contract.CoverImage.FileName,
                        Data = contract.CoverImage.Data,
                        Size = contract.CoverImage.Size
                    };
                    context.Storages.Add(storageEntity);

                    album.CoverImageId = storageEntity.Id;
                    album.CoverImage = storageEntity;
                }
            }
            var user = await context.Users.Include(u => u.AlbumFavourites).FirstOrDefaultAsync(u => u.Id == currentUserId);

            if (user != null)
            {
                if (contract.IsFavourite)
                {
                    if (!user.AlbumFavourites.Contains(album))
                        user.AlbumFavourites.Add(album);
                }
                else
                {
                    if (user.AlbumFavourites.Contains(album))
                        user.AlbumFavourites.Remove(album);
                }
            }

            await context.SaveChangesAsync();

            return Album.ToContract(album, currentUserId);
        }

        public async Task<bool> DeleteAlbumCoverAsync(Guid albumId)
        {
            var album = await context.Albums.Include(a => a.CoverImage).FirstOrDefaultAsync(a => a.Id == albumId);

            if (album == null || album.CoverImage == null)
            {
                return false;
            }

            context.Storages.Remove(album.CoverImage);

            album.CoverImage = null;
            album.CoverImageId = null;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAlbumAsync(Guid id)
        {
            var album = await context.Albums.FindAsync(id);
            if (album == null)
                return false;

            context.Albums.Remove(album);
            await context.SaveChangesAsync();

            return true;
        }

    }
}
