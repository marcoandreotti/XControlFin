using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using xControlFin.Application.Features.Financial.Commands;
using xControlFin.Application.Features.Financial.Queries;
using xControlFin.Shared.Abstractions;

namespace xControlFin.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FinancialController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public FinancialController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet("releases")]
    public async Task<IActionResult> GetReleases([FromQuery] long financialInstitutionId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var query = new GetFinancialReleasesQuery
        {
            FinancialInstitutionId = financialInstitutionId,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await _dispatcher.QueryAsync(query);
        return Ok(result);
    }

    [HttpPost("releases")]
    public async Task<IActionResult> CreateRelease([FromBody] CreateFinancialReleaseCommand command)
    {
        var id = await _dispatcher.SendAsync<long>(command);
        return Ok(new { id });
    }

    [HttpPost("planning")]
    public async Task<IActionResult> CreatePlanning([FromBody] CreateFinancialPlanningCommand command)
    {
        var id = await _dispatcher.SendAsync<long>(command);
        return Ok(new { id });
    }

    [HttpDelete("planning/{id}")]
    public async Task<IActionResult> DeletePlanning(long id)
    {
        await _dispatcher.SendAsync(new DeleteFinancialPlanningCommand { Id = id });
        return NoContent();
    }

    [HttpGet("releases/{id}")]
    public async Task<IActionResult> GetReleaseById(long id)
    {
        var result = await _dispatcher.QueryAsync(new GetFinancialReleaseByIdQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPut("releases/{id}")]
    public async Task<IActionResult> UpdateRelease(long id, [FromBody] UpdateFinancialReleaseCommand command)
    {
        command.Id = id;
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpDelete("releases/{id}")]
    public async Task<IActionResult> DeleteRelease(long id)
    {
        await _dispatcher.SendAsync(new DeleteFinancialReleaseCommand { Id = id });
        return NoContent();
    }
}