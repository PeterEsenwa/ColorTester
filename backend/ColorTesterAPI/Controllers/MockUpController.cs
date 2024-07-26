using ColorTesterAPI.Application;
using Microsoft.AspNetCore.Mvc;

namespace ColorTesterAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MockUpController : BaseController
{
    private readonly GoogleCloudStorageService _storageService;

    public MockUpController(GoogleCloudStorageService storageService)
    {
        _storageService = storageService;
    }

    [HttpGet("SignedURL/{fileName}")]
    public IActionResult GetSignedUrl(string fileName)
    {
        var signedUrl = _storageService.GetSignedUrl(fileName, TimeSpan.FromMinutes(15));
        return Ok(new { Url = signedUrl });
    }
}