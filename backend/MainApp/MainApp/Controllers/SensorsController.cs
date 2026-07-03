using Application.Services.Abstract.Sensors;
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
    public async Task<IActionResult> GetAllSensorMapping(CancellationToken cancellationToken)
    {
        return Ok(await _sensorsService.GetAllSensorMapping(cancellationToken));
    }
    
    [Authorize]
    [RequireAdmin]
    [HttpPost("renameSensor")]
    public async Task<IActionResult> RenameSensor([FromQuery] Guid mappedSensorId, [FromQuery] string sensorName, CancellationToken cancellationToken)
    {
        await _sensorsService.RenameSensor(mappedSensorId, sensorName, cancellationToken);
        return Ok();
    }
}
