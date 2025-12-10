using System;
using System.Drawing;
using Yfitops.Server.Data;
using Yfitops.Server.Models;
using Yfitops.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Yfitops.Server.Controllers;

[ApiController]
[Route("api/[controller]")]


public class TrackController : ControllerBase
{

    // Dáta z databázy
    private ApplicationDbContext context;

    // Konštruktor, kde nastavíme že práve tie dáta, ktoré nám vstupujú do funkcie sú tie, ktoré potrebujeme
    public TrackController(ApplicationDbContext context)
    {
        this.context = context;
    }

    // Asynchrónna funkcia pre získanie jedného albumu
    [HttpGet("{id}")]
    public async Task<ActionResult<TrackContract>> GetTrack(Guid id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        // Vyberieme jedného artistu podľa id z databázy = context, 
        var track = await context.Tracks.FindAsync(id);

        if (track == null)
        {
            return NotFound();
        }

        // pri komunikácii a posielaní dát zo serveru na klienta využívame DTO, vrátime jeden najdeny album
        return Ok(Track.ToContract(track, currentUserId));
    }

    [HttpGet("list-by-album/{albumId}")]
    public async Task<ActionResult<List<AlbumContract>>> GetAlbumTracks(Guid albumId)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var album = await context.Albums.Include(a => a.Tracks).ThenInclude(a => a.UserFavorites).FirstOrDefaultAsync(a => a.Id == albumId);

        if (album == null)
        {
            return Ok(new List<TrackContract>());
        }

        var contracts = album.Tracks.Select(a => Track.ToContract(a, currentUserId)).ToList();

        return Ok(contracts);
    }


    [HttpPost]
    public async Task<ActionResult<Track>> CreateTrack(TrackContract contract)
    {
        Track entity = Track.ToEntity(contract);
        context.Tracks.Add(entity);

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await context.Users
            .Include(u => u.TrackFavourites)
            .FirstOrDefaultAsync(u => u.Id == currentUserId);

        if (contract.IsFavourite)
        {
            if (!user.TrackFavourites.Contains(entity))
                user.TrackFavourites.Add(entity);
        }
        else
        {
            if (user.TrackFavourites.Contains(entity))
                user.TrackFavourites.Remove(entity);
        }


        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTrack), new { id = entity.Id }, Track.ToContract(entity, currentUserId));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTrack(Guid id, [FromBody] TrackContract contract)
    {
        if (id != contract.Id)
        {
            return BadRequest("ID mismatch");
        }


        Track track = await context.Tracks.FindAsync(id);
        if (track == null)
        {
            return NotFound();
        }

        // Aktualizujeme základné údaje
        track.Name = contract.Name;
        track.Duration = contract.Duration;

        // Získame aktuálneho používateľa
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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

        try
        {
            await context.SaveChangesAsync();
            return Ok(Track.ToContract(track, currentUserId));
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrack(Guid id)
    {
        var track = await context.Tracks.FindAsync(id);

        if (track == null)
        {
            return NotFound();
        }

        context.Tracks.Remove(track);
        try
        {
            await context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception)
        {

            return StatusCode(500, "Internal server error");
        }
    }
}

