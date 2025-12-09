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
public class ArtistController : ControllerBase
{

    // Dáta z databázy
    private ApplicationDbContext context;

    // Konštruktor, kde nastavíme že práve tie dáta, ktoré nám vstupujú do funkcie sú tie, ktoré potrebujeme
    public ArtistController (ApplicationDbContext context)
    {
        this.context = context;
    }

    // Asynchrónna funkcia pre získanie jedného Artistu
    [HttpGet("{id}")]
    public async Task<ActionResult<ArtistContract>> GetArtist(Guid id)
    {
        // Vyberieme jedného artistu podľa id z databázy = context, 
        var artist = await context.Artists.FindAsync(id);

        if (artist == null)
        {
            return NotFound();
        }

        // pri komunikácii a posielaní dát zo serveru na klienta využívame DTO, vrátime jedného nájdeného artistu
        return Ok(Artist.ToContract(artist));
    }

    // Asynchrónna funkca pre získanie všetkých artistov
    [HttpGet]
    public async Task<ActionResult<List<ArtistContract>>> GetArtists()
    {
        List<Artist> data = await context.Artists.ToListAsync();

        return Ok(data.Select(artist => Artist.ToContract(artist)));
    }

    [HttpPost]
    public async Task<ActionResult<Artist>> CreateArtist (ArtistContract contract)
    {
        Artist entity = Artist.ToEntity(contract);
        context.Artists.Add(entity);

        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetArtist), new { id = entity.Id }, Artist.ToContract(entity));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateArtist(Guid id, [FromBody] ArtistContract contract)
    {
        if (id != contract.Id)
        {
            return BadRequest("ID mismatch");
        }

        Artist artist = await context.Artists.FindAsync(id);
        if (artist == null)
        {
            return NotFound();
        }

        artist.Name = contract.Name;
        try
        {
            await context.SaveChangesAsync();
            return Ok(Artist.ToContract(artist));
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArtist(Guid id)
    {
        var artist = await context.Artists.FindAsync(id);

        if (artist == null)
        {
            return NotFound();
        }

        context.Artists.Remove(artist);
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
