using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Security.Claims;
using Yfitops.Server.Data;
using Yfitops.Server.Models;
using Yfitops.Server.Services;
using Yfitops.Shared;

namespace Yfitops.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumController : ControllerBase
{

    private readonly AlbumService service;

    public AlbumController (AlbumService service)
    {
        this.service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AlbumContract>> GetAlbum(Guid id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var album = await service.GetAlbumByIdAsync(id, currentUserId);

        if (album == null)
            return NotFound();

        return Ok(album);
    }

    [HttpGet("list-by-artist/{artistId}")]
    public async Task<ActionResult<List<AlbumContract>>> GetArtistAlbums(Guid artistId)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var albums = await service.GetArtistAlbumsAsync(artistId, currentUserId);

        return Ok(albums);
    }

    [HttpPost]
    public async Task<ActionResult<AlbumContract>> CreateAlbum(AlbumContract contract)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var created = await service.CreateAlbumAsync(contract, currentUserId);

        return CreatedAtAction(nameof(GetAlbum), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AlbumContract>> UpdateAlbum(Guid id, AlbumContract contract)
    {
        if (id != contract.Id)
            return BadRequest("ID mismatch");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var updated = await service.UpdateAlbumAsync(id, contract, currentUserId);

        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlbum(Guid id)
    {
        var deleted = await service.DeleteAlbumAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}