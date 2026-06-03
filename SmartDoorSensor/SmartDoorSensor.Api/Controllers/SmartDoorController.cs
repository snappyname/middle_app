using Application.Services.Abstractions;
using Contracts.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> SetDoorStatus(
        [FromBody] SetDoorStatusDTO doorStatus,
        [FromServices] IValidator<SetDoorStatusDTO> validator)
    {
        await _doorService.SetStatus(doorStatus);
        return Ok();
    }
}
