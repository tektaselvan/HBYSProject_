using HBYS.Core.Application.DTOs;
using HBYS.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HBYS.Core.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PatientController(PatientService patientService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await patientService.GetAllAsync(ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var patient = await patientService.GetByIdAsync(id, ct);
        return patient is null ? NotFound() : Ok(patient);
    }

    [HttpGet("tc/{tcNo}")]
    public async Task<IActionResult> GetByTcNo(string tcNo, CancellationToken ct)
    {
        var patient = await patientService.GetByTcNoAsync(tcNo, ct);
        return patient is null ? NotFound() : Ok(patient);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword, CancellationToken ct)
        => Ok(await patientService.SearchAsync(keyword, ct));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientDto dto, CancellationToken ct)
    {
        var patient = await patientService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePatientDto dto, CancellationToken ct)
        => Ok(await patientService.UpdateAsync(id, dto, ct));
}
