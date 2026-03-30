using HBYS.Core.Application.DTOs;
using HBYS.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HBYS.Core.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OrderController(OrderService orderService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var order = await orderService.GetByIdAsync(id, ct);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpGet("visit/{visitId:guid}")]
    public async Task<IActionResult> GetByVisit(Guid visitId, CancellationToken ct)
        => Ok(await orderService.GetByVisitIdAsync(visitId, ct));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto, CancellationToken ct)
        => Ok(await orderService.CreateAsync(dto, ct));

    [HttpPost("result")]
    public async Task<IActionResult> AddResult([FromBody] AddOrderResultDto dto, CancellationToken ct)
        => Ok(await orderService.AddResultAsync(dto, ct));
}
