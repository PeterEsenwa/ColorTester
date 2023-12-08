using System.Security.Claims;
using ColorTesterAPI.Application;
using ColorTesterAPI.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColorTesterAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaletteController : ControllerBase
{
    private readonly PaletteApplicationService _paletteService;

    public PaletteController(PaletteApplicationService paletteService)
    {
        _paletteService = paletteService;
    }

    [HttpPost]
    [Authorize("PaletteCreator")]
    public async Task<IActionResult> Create([FromBody] CreatePaletteRequest request)
    {
        // Extract the sub claim from the JWT token
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }
        
        var id = await _paletteService.CreatePaletteAsync(request, userIdClaim.Value);
        return CreatedAtAction(nameof(FindById), new { id }, null);
    }


    [HttpGet]
    // [Authorize("read:palettes")]
    public async Task<IActionResult> FindAll()
    {
        var palettes = await _paletteService.GetPalettesAsync();
        return Ok(palettes);
    }

    [HttpGet("{id}")]
    // [Authorize("read:palettes")]
    public Task<IActionResult> FindById(Guid id)
    {
        var paletteTask = _paletteService.GetPaletteAsync(id);

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
        var paletteTask = _paletteService.GetPaletteAsync(id);

        return paletteTask.ContinueWith<IActionResult>(task =>
        {
            var palette = task.Result;
            if (palette == null) return NotFound();
            return Ok(palette);
        });
    }

    // DELETE: api//5
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        var paletteTask = _paletteService.GetPaletteAsync(id);
    }
}