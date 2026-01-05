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
public class ArtistController : ControllerBase
{

    private readonly ArtistService service;

    public ArtistController(ArtistService service)
    {
        this.service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArtistContract>> GetArtist(Guid id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var artist = await service.GetArtistAsync(id, currentUserId);

        if (artist == null)
            return NotFound();

        return Ok(artist);
    }

    [HttpGet]
    public async Task<ActionResult<List<ArtistContract>>> GetArtists()
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var artists = await service.GetArtistsAsync(currentUserId);

        return Ok(artists);
    }

    [HttpPost]
    public async Task<ActionResult<ArtistContract>> CreateArtistAsync(ArtistContract contract)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var created = await service.CreateArtistAsync(contract, currentUserId);

        return CreatedAtAction(nameof(GetArtist), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ArtistContract>> UpdateArtist(Guid id, ArtistContract contract)
    {
        if (id != contract.Id)
            return BadRequest("ID mismatch");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var updated = await service.UpdateArtistAsync(id, contract, currentUserId);

        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArtist(Guid id)
    {
        var deleted = await service.DeleteArtistAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}