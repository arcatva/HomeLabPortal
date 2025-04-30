using HomeLabDashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeLabDashboard.Controllers;

[ApiController]
[Route("api/v1/dashboard/[controller]")]
public class SpdkController(SpdkService spdkService)
{
    [HttpGet("spdk_version")]
    public async Task<SpdkVersion> SpdkGetVersion()
    {
        var version = await spdkService.SpdkGetVersion();
        return version;
    }

    [HttpGet("framework_reactors")]
    public async Task<FrameworkReactors> FrameworkGetReactors()
    {
        var reactors = await spdkService.FrameworkGetReactors();
        return reactors;
    }

    [HttpGet("nvmf_subsystems")]
    public async Task<NvmfSubsystems> NvmfGetSubsystems()
    {
        var subsystems = await spdkService.NvmfGetSubsystems();
        return subsystems;
    }
}