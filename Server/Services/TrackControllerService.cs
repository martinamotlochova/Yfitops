using Yfitops.Server.Data;
using Yfitops.Server.Models;
using Yfitops.Shared;

namespace Yfitops.Server.Services
{
    public class TrackControllerService
    {
        private ApplicationDbContext context;

        public TrackControllerService(ApplicationDbContext context)
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
    }
}
