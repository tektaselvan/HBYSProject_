using HBYS.Core.Application.DTOs;
using HBYS.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HBYS.Core.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class VisitController(VisitService visitService) : ControllerBase
{
    [HttpGet("today")]
    public async Task<IActionResult> GetTodaysVisits(CancellationToken ct)
        => Ok(await visitService.GetTodaysVisitsAsync(ct));

    [HttpGet("patient/{patientId:guid}")]
    public async Task<IActionResult> GetByPatient(Guid patientId, CancellationToken ct)
        => Ok(await visitService.GetByPatientIdAsync(patientId, ct));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVisitDto dto, CancellationToken ct)
        => Ok(await visitService.CreateAsync(dto, ct));

    [HttpPatch("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id, CancellationToken ct)
        => Ok(await visitService.CompleteAsync(id, ct));

    [HttpPatch("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken ct)
        => Ok(await visitService.CancelAsync(id, ct));
}
