using System.Security.Claims;
using ColorTesterAPI.Application;
using ColorTesterAPI.Application.DTOs;
using ColorTesterAPI.Domain.Entities;
using ColorTesterAPI.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColorTesterAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaletteController : BaseController
{
    private readonly PaletteApplicationService _paletteService;

    public PaletteController(PaletteApplicationService paletteService)
    {
        _paletteService = paletteService;
    }

    [HttpPost]
    // [Authorize("PaletteCreator")]
    public async Task<IActionResult> Create([FromBody] CreatePaletteRequest request)
    {
        if (UserId == null)
        {
            return Unauthorized();
        }
        
        var id = await _paletteService.CreatePaletteAsync(request, UserId);
        return CreatedAtAction(nameof(FindById), new { id }, null);
    }


    [HttpGet]
    // FindALl for a specific user
    [Authorize("PaletteReader")]
    public async Task<IActionResult> FindAll()
    {
        if (UserId == null)
        {
            return Unauthorized();
        }
        
        var palettes = await _paletteService.GetPalettesAsync(UserId);
        return Ok(palettes);
    }
    
    [HttpGet("{id}")]
    [Authorize("PaletteReader")]
    public Task<IActionResult> FindById(Guid id)
    {
        if (UserId == null)
        {
            return Task.FromResult<IActionResult>(Unauthorized());
        }

        var paletteTask = _paletteService.GetPaletteAsync(id, UserId);

        return paletteTask.ContinueWith<IActionResult>(task =>
        {
            var palette = task.Result;
            if (palette == null) return NotFound();
            return Ok(palette);
        });
    }

    [HttpPut("{id}")]
    public Task<IActionResult> Update(Guid id, [FromBody] CreatePaletteRequest request)
    {
        if (UserId == null)
        {
            return Task.FromResult<IActionResult>(Unauthorized());
        }
        
        var paletteTask = _paletteService.GetPaletteAsync(id, UserId);
        return paletteTask.ContinueWith<IActionResult>(task =>
        {
            var palette = task.Result;
            if (palette == null) return NotFound();
            palette.Name = request.Name;
            
            // convert PaletteColorDTO to PaletteColor
            palette.Colors = request.Colors.ToDictionary(
                c => c.Role,
                c => new PaletteColor(c.Role, new Color(9, 2, 3))
            );
            return Ok();
        });
    }

    // DELETE: api//5
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        if (UserId == null)
        {
            return;
        }
        var paletteTask = _paletteService.GetPaletteAsync(id, UserId);
    }
}