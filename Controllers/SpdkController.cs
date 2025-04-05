using HomeLabPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeLabPortal.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SpdkController(SpdkService spdkService)
{
    [HttpGet("version")]
    public async Task<SpdkVersion> GetVersion()
    {
        var version = await spdkService.GetVersion();
        return version;
    }
}