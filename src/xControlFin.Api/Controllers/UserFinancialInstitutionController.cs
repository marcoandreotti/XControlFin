using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using xControlFin.Application.Features.UserFinancialInstitutions.Commands;
using xControlFin.Application.Features.UserFinancialInstitutions.Queries;
using xControlFin.Shared.Abstractions;

namespace xControlFin.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserFinancialInstitutionController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public UserFinancialInstitutionController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(long userId)
    {
        var result = await _dispatcher.QueryAsync(new GetFinancialInstitutionsByUserIdQuery { UserId = userId });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserFinancialInstitutionCommand command)
    {
        var id = await _dispatcher.SendAsync<long>(command);
        return Ok(new { id });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _dispatcher.SendAsync(new DeleteUserFinancialInstitutionCommand { Id = id });
        return NoContent();
    }
}