using Application.Services.Abstract.Sensors;
using Contracts.Frontend.Sensors;
using MainApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorsController : ControllerBase
{
    private readonly ISensorsService _sensorsService;

    public SensorsController(ISensorsService sensorsService)
    {
        _sensorsService = sensorsService;
    }
    
    [Authorize]
    [RequireAdmin]
    [HttpGet("allSensors")]
    public async Task<IActionResult> GetAllSensorMapping()
    {
        return Ok(await _sensorsService.GetAllSensorMapping());
    }
    
    [Authorize]
    [RequireAdmin]
    [HttpPost("renameSensor")]
    public async Task<IActionResult> RenameSensor([FromQuery] Guid mappedSensorId, [FromQuery] string sensorName = "")
    {
        await _sensorsService.RenameSensor(mappedSensorId, sensorName);
        return Ok();
    }
}
