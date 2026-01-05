using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yfitops.Server.Data;
using Yfitops.Server.Models;
using Yfitops.Shared;


namespace Yfitops.Server.Services
{
    public class ArtistService
    {
        private ApplicationDbContext context;

        public ArtistService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ArtistContract> GetArtistAsync(Guid id, string currentUserId)
        {
            var artist = await context.Artists.FirstOrDefaultAsync(a => a.Id == id);

            if (artist == null)
            {
                return null;
            }

            return Artist.ToContract(artist, currentUserId);
        }

        public async Task<List<ArtistContract>> GetArtistsAsync(string currentUserId)
        {
            var artists = await context.Artists.Include(a => a.UserFavorites).ToListAsync();
            var contracts = artists.Select(a => Artist.ToContract(a, currentUserId)).ToList();
            return contracts;
        }

        public async Task<ArtistContract> CreateArtistAsync(ArtistContract contract, string currentUserId)
        {

            Artist entity = Artist.ToEntity(contract);
            context.Artists.Add(entity);

            var user = await context.Users.Include(u => u.ArtistFavourites).FirstOrDefaultAsync(u => u.Id == currentUserId);

            if (contract.IsFavourite)
            {
                if (!user.ArtistFavourites.Contains(entity))
                    user.ArtistFavourites.Add(entity);
            }
            else
            {
                if (user.ArtistFavourites.Contains(entity))
                    user.ArtistFavourites.Remove(entity);
            }

            await context.SaveChangesAsync();

            return Artist.ToContract(entity, currentUserId);
        }

        public async Task<ArtistContract> UpdateArtistAsync(Guid id, ArtistContract contract, string currentUserId)
        {
            if (id != contract.Id)
            {
                throw new ArgumentException("ID mismatch");
            }

            var artist = await context.Artists.FirstOrDefaultAsync(a => a.Id == id);
            if (artist == null)
            {
                throw new KeyNotFoundException("Artist not found");
            }

            artist.Name = contract.Name;

            var user = await context.Users
                .Include(u => u.ArtistFavourites)
                .FirstOrDefaultAsync(u => u.Id == currentUserId);

            if (user != null)
            {
                if (contract.IsFavourite)
                {
                    if (!user.ArtistFavourites.Contains(artist))
                        user.ArtistFavourites.Add(artist);
                }
                else
                {
                    if (user.ArtistFavourites.Contains(artist))
                        user.ArtistFavourites.Remove(artist);
                }
            }

            await context.SaveChangesAsync();
            return Artist.ToContract(artist, currentUserId);
        }

        public async Task<bool> DeleteArtistAsync(Guid id)
        {
            var artist = await context.Artists.FirstOrDefaultAsync(a => a.Id == id);
            if (artist == null)
            {
                return false;
            }

            context.Artists.Remove(artist);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
