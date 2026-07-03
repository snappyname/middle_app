using Application.Services.Abstract.Admin;
using Contracts.Frontend.Admin;
using Contracts.Frontend.Sensors;
using MainApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [Authorize]
    [RequireAdmin]
    [HttpGet("allUsers")]
    public async Task<List<UserDTO>> GetAllUsers(CancellationToken cancellationToken)
    {
        return await _adminService.GetAllUsersWithSensorsAsync(cancellationToken);
    }

    [Authorize]
    [RequireAdmin]
    [HttpPost("updateUser")]
    public async Task<IActionResult> UpdateUser([FromQuery] Guid userId, [FromQuery] string userName, [FromQuery] bool isAdmin, CancellationToken cancellationToken)
    {
        await _adminService.UpdateUserAsync(userId, userName, isAdmin, cancellationToken);
        return Ok();
    }

    [Authorize]
    [RequireAdmin]
    [HttpPost("addNewSensor")]
    public async Task<IActionResult> AddNewSensor([FromBody] SensorDTO sensor, CancellationToken cancellationToken)
    {
        var id = await _adminService.AddNewSensorAsync(sensor, cancellationToken);
        return Ok(id);
    }

    [Authorize]
    [RequireAdmin]
    [HttpGet("getAllSensorsMap")]
    public async Task<IActionResult> GetAllSensorsMap(CancellationToken cancellationToken)
    {
        return Ok(await _adminService.GetAllSensorsAsync(cancellationToken));
    }

    [Authorize]
    [RequireAdmin]
    [HttpPost("assignSensors")]
    public async Task<IActionResult> AssignSensors([FromBody] List<SensorDTO> sensors, CancellationToken cancellationToken)
    {
        await _adminService.UpdateUserSensorsAsync(sensors, cancellationToken);
        return Ok();
    }
}
