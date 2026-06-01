using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace TemperatureSensor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemperatureController : ControllerBase
{
    private readonly ITemperatureService _temperatureService;

    public TemperatureController(ITemperatureService temperatureService)
    {
        _temperatureService = temperatureService;
    }

    [HttpGet]
    public Task<IActionResult> GetTemperature()
    {
        return Task.FromResult<IActionResult>(Ok(_temperatureService.GetTemperature()));
    }
}
