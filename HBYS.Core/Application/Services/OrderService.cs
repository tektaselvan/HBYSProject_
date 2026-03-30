using HBYS.Core.Application.DTOs;
using HBYS.Core.Application.Interfaces;
using HBYS.Core.Domain.Entities;

namespace HBYS.Core.Application.Services;

public class OrderService(IOrderRepository orderRepository)
{
    public async Task<OrderDto> CreateAsync(CreateOrderDto dto, CancellationToken ct = default)
    {
        var order = Order.Create(dto.VisitId, dto.Notes);
        foreach (var d in dto.Details)
            order.AddDetail(d.TestCode, d.TestName);

        await orderRepository.AddAsync(order, ct);
        return ToDto(order);
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var order = await orderRepository.GetByIdWithDetailsAsync(id, ct);
        return order is null ? null : ToDto(order);
    }

    public async Task<IEnumerable<OrderDto>> GetByVisitIdAsync(Guid visitId, CancellationToken ct = default)
    {
        var orders = await orderRepository.GetByVisitIdAsync(visitId, ct);
        return orders.Select(ToDto);
    }

    public async Task<OrderDto> AddResultAsync(AddOrderResultDto dto, CancellationToken ct = default)
    {
        var result = OrderResult.Create(dto.OrderDetailId, dto.ResultValue,
            dto.Unit, dto.ReferenceRange, dto.IsAbnormal);

        await orderRepository.AddResultAsync(result, ct);
        var order = await orderRepository.GetByIdWithDetailsAsync(
            (await orderRepository.GetByIdAsync(dto.OrderDetailId, ct))!.Id, ct);
        return ToDto(order!);
    }

    private static OrderDto ToDto(Order o) => new(
        o.Id, o.VisitId, o.OrderDate, o.Status, o.Notes,
        o.OrderDetails.Select(d => new OrderDetailDto(
            d.Id, d.TestCode, d.TestName, d.Status,
            d.Result is null ? null : new OrderResultDto(
                d.Result.Id, d.Result.ResultValue, d.Result.Unit,
                d.Result.ReferenceRange, d.Result.IsAbnormal, d.Result.ResultDate))));
}
