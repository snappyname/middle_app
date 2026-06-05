using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace HumiditySensor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HumidityController : ControllerBase
{
    private readonly IHumidityService _humidityService;

    public HumidityController(IHumidityService humidityService)
    {
        _humidityService = humidityService;
    }

    [HttpGet]
    public Task<IActionResult> GetHumidity()
    {
        return Task.FromResult<IActionResult>(Ok(_humidityService.GetHumidity()));
    }
}
