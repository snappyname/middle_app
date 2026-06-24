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
    public async Task<List<UserDTO>> GetAllUsers()
    {
        return await _adminService.GetAllUsersAsync();
    }

    [Authorize]
    [RequireAdmin]
    [HttpPost("updateUser")]
    public async Task<IActionResult> UpdateUser([FromQuery] Guid userId, [FromQuery] string userName,
        [FromQuery] bool isAdmin)
    {
        await _adminService.UpdateUserAsync(userId, userName, isAdmin);
        return Ok();
    }

    [Authorize]
    [RequireAdmin]
    [HttpPost("addNewSensor")]
    public async Task<IActionResult> AddNewSensor([FromBody] SensorDTO sensor)
    {
        var id = await _adminService.AddNewSensorAsync(sensor);
        return Ok(id);
    }

    [Authorize]
    [RequireAdmin]
    [HttpGet("getAllSensorsMap")]
    public async Task<IActionResult> GetAllSensorsMap()
    {
        return Ok(await _adminService.GetAllSensorsAsync());
    }
}
