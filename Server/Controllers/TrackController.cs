using System;
using System.Drawing;
using Yfitops.Server.Data;
using Yfitops.Server.Models;
using Yfitops.Server.Services;
using Yfitops.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Yfitops.Server.Controllers;

[ApiController]
[Route("api/[controller]")]


public class TrackController : ControllerBase
{

    private readonly TrackService trackService;

    public TrackController(TrackService trackService)
    {
        this.trackService = trackService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrackContract>> GetTrack(Guid id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var track = await trackService.GetTrackByIdAsync(id, currentUserId);

        if (track == null)
            return NotFound();

        return Ok(track);
    }

    [HttpGet("list-by-album/{albumId}")]
    public async Task<ActionResult<List<TrackContract>>> GetAlbumTracks(Guid albumId)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var tracks = await trackService.GetAlbumTracksAsync(albumId, currentUserId);

        return Ok(tracks);
    }

    [HttpGet("{id}/stream")]
    public async Task<IActionResult> StreamTrack(Guid id)
    {
        var result = await trackService.GetTrackStreamAsync(id);

        if (result == null)
            return NotFound(new { message = "Track or audio file not found" });

        var (stream, contentType, fileName) = result.Value;

        return File(
            stream,
            contentType,
            fileName,
            enableRangeProcessing: true 
        );
    }

    [HttpPost]
    public async Task<ActionResult<TrackContract>> CreateTrack(TrackContract contract)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var created = await trackService.CreateTrackAsync(contract, currentUserId);

        return CreatedAtAction(nameof(GetTrack),new { id = created.Id },created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TrackContract>> UpdateTrack(Guid id, TrackContract contract)
    {
        if (id != contract.Id)
            return BadRequest("ID mismatch");

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var updated = await trackService.UpdateTrackAsync(id, contract, currentUserId);

        if (updated == null)
            return NotFound();

        return Ok(updated);
    }
    [HttpDelete("{trackId}/storage")]
    public async Task<IActionResult> DeleteTrackStorage(Guid trackId)
    {
        var deleted = await trackService.DeleteTrackStorage(trackId);

        if (!deleted)
            return NotFound();

        return NoContent();

    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrack(Guid id)
    {
        var deleted = await trackService.DeleteTrackAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}

