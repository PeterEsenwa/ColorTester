using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ColorTesterAPI.Controllers;

public abstract class BaseController : ControllerBase
{
    protected string? UserId => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
}