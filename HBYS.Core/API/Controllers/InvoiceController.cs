using HBYS.Core.Application.DTOs;
using HBYS.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HBYS.Core.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class InvoiceController(InvoiceService invoiceService) : ControllerBase
{
    [HttpGet("visit/{visitId:guid}")]
    public async Task<IActionResult> GetByVisit(Guid visitId, CancellationToken ct)
    {
        var invoice = await invoiceService.GetByVisitIdAsync(visitId, ct);
        return invoice is null ? NotFound() : Ok(invoice);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceDto dto, CancellationToken ct)
        => Ok(await invoiceService.CreateAsync(dto, ct));

    [HttpPost("{id:guid}/detail")]
    public async Task<IActionResult> AddDetail(Guid id, [FromBody] AddInvoiceDetailDto dto, CancellationToken ct)
        => Ok(await invoiceService.AddDetailAsync(id, dto, ct));

    [HttpPatch("{id:guid}/send-sgk")]
    public async Task<IActionResult> SendToSGK(Guid id, CancellationToken ct)
        => Ok(await invoiceService.SendToSGKAsync(id, ct));
}
