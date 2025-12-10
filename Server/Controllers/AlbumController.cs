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
public class AlbumController : ControllerBase
{

    // Dáta z databázy
    private ApplicationDbContext context;

    // Konštruktor, kde nastavíme že práve tie dáta, ktoré nám vstupujú do funkcie sú tie, ktoré potrebujeme
    public AlbumController (ApplicationDbContext context)
    {
        this.context = context;
    }

    // Asynchrónna funkcia pre získanie jedného albumu
    [HttpGet("{id}")]
    public async Task<ActionResult<AlbumContract>> GetAlbum(Guid id)
    {
        // Vyberieme jedného artistu podľa id z databázy = context, 
        var album = await context.Albums.FindAsync(id);

        if (album == null)
        {
            return NotFound();
        }

        // pri komunikácii a posielaní dát zo serveru na klienta využívame DTO, vrátime jeden najdeny album
        return Ok(Album.ToContract(album));
    }

    // Asynchrónna funkca pre získanie všetkých artistov
    //[HttpGet]
    //public async Task<ActionResult<List<AlbumContract>>> GetAlbums()
    //{
        //List<Album> data = await context.Albums.ToListAsync();

        //return Ok(data.Select(album => Album.ToContract(album)));
    //}

    // Endpoint, ktory prijima artistId ako GUID a vracia zoznam albumov daneho artistu
    [HttpGet("list-by-artist/{artistId}")]
    public async Task<ActionResult<List<AlbumContract>>> GetArtistAlbums(Guid artistId)
    {
        var artist = await context.Artists.Include(a => a.Albums).FirstOrDefaultAsync(a => a.Id == artistId);

        if (artist == null)
        {
            return Ok(new List<AlbumContract>());
        }
        return Ok(artist.Albums.Select(Album.ToContract).ToList());
    }

    [HttpPost]
    public async Task<ActionResult<Album>> CreateAlbum (AlbumContract contract)
    {
        Album entity = Album.ToEntity(contract);
        context.Albums.Add(entity);

        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAlbum), new { id = entity.Id }, Album.ToContract(entity));
    }

    [HttpPut("{id}")]
    
    public async Task<ActionResult> UpdateAlbum (Guid id, [FromBody] AlbumContract contract)
    {
        if (id != contract.Id)
        {
            return BadRequest("ID mismatch");
        }

        Album album = await context.Albums.FindAsync(id);
        if (album == null)
        {
            return NotFound();
        }

        album.Name = contract.Name;
        album.ReleaseDate = contract.ReleaseDate.Value;
        try
        {
            await context.SaveChangesAsync();
            return Ok(Album.ToContract(album));
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
