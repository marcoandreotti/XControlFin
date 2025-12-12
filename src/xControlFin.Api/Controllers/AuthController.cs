using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using xControlFin.Application.Features.Auth.Commands;
using xControlFin.Application.Features.Auth.Dtos;
using xControlFin.Shared.Abstractions;

namespace xControlFin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public AuthController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        try
        {
            var result = await _dispatcher.SendAsync<AuthResponseDto>(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
    {
        try
        {
            var result = await _dispatcher.SendAsync<AuthResponseDto>(command);
            return Ok(result);
        }
        catch (Exception)
        {
            return Unauthorized(new { message = "Invalid token" });
        }
    }
}