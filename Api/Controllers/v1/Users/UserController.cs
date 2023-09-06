using Api.Helpers;
using Api.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1.Users;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly JwtSettings _jwtSettings;

    public UserController(ILogger<UserController> logger, JwtSettings jwtSettings)
    {
        _logger = logger;
        _jwtSettings = jwtSettings;
    }

    [HttpGet("create_token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Guid userId)
    {
        try
        {
            if (!ModelState.IsValid)
                throw new ArgumentException("Model not valid");
            
            string newToken = JwtHelper.GenerateAccessToken(_jwtSettings, userId);
            
            return Ok(new { status = 200, description = "Token was created", token = newToken });
        }
        catch (Exception e)
        {
            _logger.LogInformation(e.Message);
            return BadRequest(new { status = 400, description = e.Message });
        }
    }


    [Authorize]
    [HttpGet("get_info")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(new { status = 200, description = "Token valid" });
        }
        catch (Exception e)
        {
            _logger.LogInformation(e.Message);
            return BadRequest(new { status = 400, description = e.Message });
        }
    }
}