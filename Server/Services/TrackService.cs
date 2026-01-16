using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using Yfitops.Client.Pages;
using Yfitops.Server.Data;
using Yfitops.Server.Models;
using Yfitops.Shared;

namespace Yfitops.Server.Services
{
    public class TrackService
    {
        private ApplicationDbContext context;

        public TrackService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<TrackContract>> GetAllTracksAsync(string currentUserId)
            => await context.Tracks
                .Include(a => a.Storage)
                .Include(a => a.UserFavorites)
                .Select(a => Track.ToContract(a, currentUserId))
                .ToListAsync();

        public async Task<TrackContract> GetTrackByIdAsync(Guid id, string currentUserId)
        {
            var track = await context.Tracks.FindAsync(id);
            if (track == null)
            {
                return null;
            }
            return Track.ToContract(track, currentUserId);
        }

        public async Task<List<TrackContract>> GetAlbumTracksAsync(Guid albumId, string currentUserId)
        {
            var album = await context.Albums.Include(a => a.Tracks).ThenInclude(a => a.UserFavorites).FirstOrDefaultAsync(a => a.Id == albumId);

            if (album == null)
            {
                return new List<TrackContract>();
            }

            return album.Tracks.Select(a => Track.ToContract(a, currentUserId)).ToList();
        }
        public async Task<(Stream stream, string contentType, string fileName)?> GetTrackStreamAsync(Guid trackId)
        {
            var track = await context.Tracks
                .Include(t => t.Storage)
                .FirstOrDefaultAsync(t => t.Id == trackId);

            if (track?.Storage == null)
                return null;

            var stream = new MemoryStream(track.Storage.Data);
            var contentType = GetContentType(track.Storage.FileName);

            return (stream, contentType, track.Storage.FileName);
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();

            return extension switch
            {
                ".mp3" => "audio/mpeg",
                ".wav" => "audio/wav",
                ".ogg" => "audio/ogg",
                ".m4a" => "audio/mp4",
                ".flac" => "audio/flac",
                ".aac" => "audio/aac",
                _ => "application/octet-stream"
            };
        }

        public async Task<TrackContract> CreateTrackAsync(TrackContract track, string currentUserId)
        {
            Storage storageEntity = null;

            if(track.Storage != null)
            {
                storageEntity = new Storage
                {
                    Id = Guid.NewGuid(),
                    FileName = track.Storage.FileName,
                    Data = track.Storage.Data,
                    Size = track.Storage.Size
                };

                context.Storages.Add(storageEntity);
            }

            Track entity = Track.ToEntity(track);

            if (storageEntity != null)
            {
                entity.StorageId = storageEntity.Id;
                entity.Storage = storageEntity;
            }
            context.Tracks.Add(entity);

            var user = await context.Users.Include(u => u.TrackFavourites).FirstOrDefaultAsync(u => u.Id == currentUserId);
            if (user != null)
            {
                if (track.IsFavourite)
                {
                    if (!user.TrackFavourites.Contains(entity))
                        user.TrackFavourites.Add(entity);
                }
                else
                {
                    if (user.TrackFavourites.Contains(entity))
                        user.TrackFavourites.Remove(entity);
                }
            }

            await context.SaveChangesAsync();

            return Track.ToContract(entity, currentUserId);
        }

        public async Task<TrackContract> UpdateTrackAsync(Guid id,TrackContract contract,string currentUserId)
        {
            var track = await context.Tracks.FindAsync(id);
            if (track == null)
                return null;

            track.Name = contract.Name;
            track.Duration = contract.Duration;

            if (contract.Storage != null)
            {
                Storage storageEntity;

                if (track.Storage != null)
                {
                    storageEntity = track.Storage;
                    storageEntity.FileName = contract.Storage.FileName;
                    storageEntity.Data = contract.Storage.Data;
                    storageEntity.Size = contract.Storage.Size;
                }
                else
                { 
                    storageEntity = new Storage
                    {
                        Id = Guid.NewGuid(),
                        FileName = contract.Storage.FileName,
                        Data = contract.Storage.Data,
                        Size = contract.Storage.Size
                    };
                    context.Storages.Add(storageEntity);

                    track.StorageId = storageEntity.Id;
                    track.Storage = storageEntity;
                }
            }

            var user = await context.Users
                .Include(u => u.TrackFavourites)
                .FirstOrDefaultAsync(u => u.Id == currentUserId);

            if (user != null)
            {
                if (contract.IsFavourite)
                {
                    if (!user.TrackFavourites.Contains(track))
                        user.TrackFavourites.Add(track);
                }
                else
                {
                    if (user.TrackFavourites.Contains(track))
                        user.TrackFavourites.Remove(track);
                }
            }

            await context.SaveChangesAsync();

            return Track.ToContract(track, currentUserId);
        }

        public async Task<bool> DeleteTrackStorage (Guid trackId)
        {
            var track = await context.Tracks.Include(t => t.Storage).FirstOrDefaultAsync(t => t.Id == trackId);

            if(track == null || track.Storage == null)
            {
                return false;
            }
            context.Storages.Remove(track.Storage);

            track.Storage = null;
            track.StorageId = null;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTrackAsync(Guid id)
        {
            var track = await context.Tracks.FindAsync(id);
            if (track == null)
                return false;

            context.Tracks.Remove(track);
            await context.SaveChangesAsync();

            return true;
        }

    }
}
