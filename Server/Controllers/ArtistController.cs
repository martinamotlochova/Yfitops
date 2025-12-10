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
public class ArtistController : ControllerBase
{

    // Dáta z databázy
    private ApplicationDbContext context;

    // Konštruktor, kde nastavíme že práve tie dáta, ktoré nám vstupujú do funkcie sú tie, ktoré potrebujeme
    public ArtistController(ApplicationDbContext context)
    {
        this.context = context;
    }

    // Asynchrónna funkcia pre získanie jedného Artistu
    [HttpGet("{id}")]
    public async Task<ActionResult<ArtistContract>> GetArtist(Guid id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        // Vyberieme jedného artistu podľa id z databázy = context, 
        var artist = await context.Artists.FindAsync(id);

        if (artist == null)
        {
            return NotFound();
        }

        // pri komunikácii a posielaní dát zo serveru na klienta využívame DTO, vrátime jedného nájdeného artistu
        return Ok(Artist.ToContract(artist, currentUserId));
    }

    // Asynchrónna funkca pre získanie všetkých artistov
    [HttpGet]
    public async Task<ActionResult<List<ArtistContract>>> GetArtists()
    {
        // kto je prihlásený (môže byť null ak anonymous)
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // načítame artistov + navigačnú kolekciu používateľov, ktorí si obľúbili daného artistu
        var artists = await context.Artists
            .Include(a => a.UserFavorites)   // <-- uprav názov podľa tvojho modelu
            .ToListAsync();

        // mapujeme na contract a vrátime list
        var contracts = artists
            .Select(a => Artist.ToContract(a, currentUserId))
            .ToList();

        return Ok(contracts);
    }


    [HttpPost]
    public async Task<ActionResult<Artist>> CreateArtist(ArtistContract contract)
    {

        Artist entity = Artist.ToEntity(contract);
        context.Artists.Add(entity);

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await context.Users
            .Include(u => u.ArtistFavourites) // názov kolekcie prispôsob podľa svojej entity
            .FirstOrDefaultAsync(u => u.Id == currentUserId);

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

        // Ulož zmeny v kolekcii
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetArtist), new { id = entity.Id }, Artist.ToContract(entity, currentUserId));
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


        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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

        try
        {
            await context.SaveChangesAsync();
            return Ok(Artist.ToContract(artist, currentUserId));
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
