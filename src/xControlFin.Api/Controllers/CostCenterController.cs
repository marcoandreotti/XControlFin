using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using xControlFin.Application.Features.CostCenters.Commands;
using xControlFin.Application.Features.CostCenters.Queries;
using xControlFin.Shared.Abstractions;

namespace xControlFin.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CostCenterController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public CostCenterController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _dispatcher.QueryAsync(new GetAllCostCentersQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _dispatcher.QueryAsync(new GetCostCenterByIdQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCostCenterCommand command)
    {
        var id = await _dispatcher.SendAsync<long>(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateCostCenterCommand command)
    {
        command.Id = id;
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _dispatcher.SendAsync(new DeleteCostCenterCommand { Id = id });
        return NoContent();
    }
}