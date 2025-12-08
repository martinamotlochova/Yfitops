using System;
using System.Drawing;
using Yfitops.Server.Data;
using Yfitops.Server.Models;
using Yfitops.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Yfitops.Server.Controllers;

[ApiController]
[Route("api/[controller]")]


public class TrackController : ControllerBase
{

    // Dáta z databázy
    private ApplicationDbContext context;

    // Konštruktor, kde nastavíme že práve tie dáta, ktoré nám vstupujú do funkcie sú tie, ktoré potrebujeme
    public TrackController (ApplicationDbContext context)
    {
        this.context = context;
    }

    // Asynchrónna funkcia pre získanie jedného albumu
    [HttpGet("{id}")]
    public async Task<ActionResult<TrackContract>> GetTrack(Guid id)
    {
        // Vyberieme jedného artistu podľa id z databázy = context, 
        var track = await context.Tracks.FindAsync(id);

        if (track == null)
        {
            return NotFound();
        }

        // pri komunikácii a posielaní dát zo serveru na klienta využívame DTO, vrátime jeden najdeny album
        return Ok(Track.ToContract(track));
    }

    // Asynchrónna funkca pre získanie všetkých artistov
    [HttpGet]
    public async Task<ActionResult<List<TrackContract>>> GetTracks()
    {
        List<Track> data = await context.Tracks.ToListAsync();

        return Ok(data.Select(track => Track.ToContract(track)));
    }

    // Endpoint, ktory prijima artistId ako GUID a vracia zoznam albumov daneho artistu
    [HttpGet("{albumId}/tracks")]
    public async Task<ActionResult<List<TrackContract>>> GetAlbumTracks(Guid albumId)
    {
        var album = await context.Albums.Include(a => a.Tracks).FirstOrDefaultAsync(a => a.Id == albumId);

        if (album == null)
        {
            return NotFound();
        }
        return Ok(album.Tracks.Select(Track.ToContract).ToList());
    }


    [HttpPost("{albumId}/tracks")]
    public async Task<ActionResult<Album>> CreateTrackAlbum (Guid albumId, [FromBody] TrackContract contract)
    {
        var album  = await context.Albums.FindAsync(albumId);

        if(album == null)
        {
            return NotFound();
        }

        var track = Track.ToEntity(contract);
        track.Id = Guid.NewGuid();
        track.AlbumId = albumId;

        context.Tracks.Add(track);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAlbumTracks), new { albumId = albumId }, Track.ToContract(track));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTrack (Guid id, [FromBody] TrackContract contract)
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

        track.Name = contract.Name;
        track.Duration = contract.Duration;
        try
        {
            await context.SaveChangesAsync();
            return Ok(Track.ToContract(track));
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

