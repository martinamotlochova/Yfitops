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
public class AlbumController : ControllerBase
{

    // Dáta z databázy
    private ApplicationDbContext context;

    // Konštruktor, kde nastavíme že práve tie dáta, ktoré nám vstupujú do funkcie sú tie, ktoré potrebujeme
    public AlbumController(ApplicationDbContext context)
    {
        this.context = context;
    }

    // Asynchrónna funkcia pre získanie jedného albumu
    [HttpGet("{id}")]
    public async Task<ActionResult<AlbumContract>> GetAlbum(Guid id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        // Vyberieme jedného artistu podľa id z databázy = context, 
        var album = await context.Albums.FindAsync(id);

        if (album == null)
        {
            return NotFound();
        }

        // pri komunikácii a posielaní dát zo serveru na klienta využívame DTO, vrátime jeden najdeny album
        return Ok(Album.ToContract(album, currentUserId));
    }

    // Endpoint, ktory prijima artistId ako GUID a vracia zoznam albumov daneho artistu
    [HttpGet("list-by-artist/{artistId}")]
    public async Task<ActionResult<List<AlbumContract>>> GetArtistAlbums(Guid artistId)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var artist = await context.Artists.Include(a => a.Albums).ThenInclude(a => a.UserFavorites).FirstOrDefaultAsync(a => a.Id == artistId);

        if (artist == null)
        {
            return Ok(new List<AlbumContract>());
        }

        var contracts = artist.Albums.Select(a => Album.ToContract(a, currentUserId)).ToList();

        return Ok(contracts);
    }


    [HttpPost]
    public async Task<ActionResult<Album>> CreateAlbum(AlbumContract contract)
    {
        Album entity = Album.ToEntity(contract);
        context.Albums.Add(entity);

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await context.Users
            .Include(u => u.AlbumFavourites)
            .FirstOrDefaultAsync(u => u.Id == currentUserId);

        if (contract.IsFavourite)
        {
            if (!user.AlbumFavourites.Contains(entity))
                user.AlbumFavourites.Add(entity);
        }
        else
        {
            if (user.AlbumFavourites.Contains(entity))
                user.AlbumFavourites.Remove(entity);
        }


        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAlbum), new { id = entity.Id }, Album.ToContract(entity, currentUserId));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAlbum(Guid id, [FromBody] AlbumContract contract)
    {
        if (id != contract.Id)
        {
            return BadRequest("ID mismatch");
        }

        // Najprv načítame album
        Album album = await context.Albums.FindAsync(id);
        if (album == null)
        {
            return NotFound();
        }

        // Aktualizujeme základné údaje
        album.Name = contract.Name;
        album.ReleaseDate = contract.ReleaseDate;

        // Získame aktuálneho používateľa
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await context.Users
            .Include(u => u.AlbumFavourites)
            .FirstOrDefaultAsync(u => u.Id == currentUserId);

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

        try
        {
            await context.SaveChangesAsync();
            return Ok(Album.ToContract(album, currentUserId));
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlbum(Guid id)
    {
        var album = await context.Albums.FindAsync(id);

        if (album == null)
        {
            return NotFound();
        }

        context.Albums.Remove(album);
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
