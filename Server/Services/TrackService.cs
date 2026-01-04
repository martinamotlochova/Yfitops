using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
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

        public async Task<TrackContract> CreateTrackAsync(TrackContract track, string currentUserId)
        {
            Track entity = Track.ToEntity(track);
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
