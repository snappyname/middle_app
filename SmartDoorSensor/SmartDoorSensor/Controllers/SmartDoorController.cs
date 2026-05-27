using Microsoft.AspNetCore.Mvc;
using SmartDoorSensor.DTO;
using SmartDoorSensor.Services.Abstractions;

namespace SmartDoorSensor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SmartDoorController : ControllerBase
{
    private readonly IDoorService _doorService;

    public SmartDoorController(IDoorService doorService)
    {
        _doorService = doorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDoorStatus()
    {
        return Ok(await _doorService.GetStatus());
    }

    [HttpPost]
    public async Task<IActionResult> SetDoorStatus([FromBody] SetDoorStatusDTO doorStatus)
    {
        await _doorService.SetStatus(doorStatus);
        return Ok();
    }
}
