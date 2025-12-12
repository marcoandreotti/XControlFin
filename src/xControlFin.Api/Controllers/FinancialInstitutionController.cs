using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using xControlFin.Application.Features.FinancialInstitutions.Commands;
using xControlFin.Application.Features.FinancialInstitutions.Queries;
using xControlFin.Shared.Abstractions;

namespace xControlFin.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FinancialInstitutionController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public FinancialInstitutionController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _dispatcher.QueryAsync(new GetAllFinancialInstitutionsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _dispatcher.QueryAsync(new GetFinancialInstitutionByIdQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFinancialInstitutionCommand command)
    {
        var id = await _dispatcher.SendAsync<long>(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateFinancialInstitutionCommand command)
    {
        command.Id = id;
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _dispatcher.SendAsync(new DeleteFinancialInstitutionCommand { Id = id });
        return NoContent();
    }
}