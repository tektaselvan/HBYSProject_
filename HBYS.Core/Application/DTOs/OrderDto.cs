using HBYS.Core.Domain.Enums;

namespace HBYS.Core.Application.DTOs;

public record OrderDto(
    Guid Id, Guid VisitId, DateTime OrderDate,
    OrderStatus Status, string? Notes,
    IEnumerable<OrderDetailDto> Details);

public record OrderDetailDto(
    Guid Id, string TestCode, string TestName,
    OrderStatus Status, OrderResultDto? Result);

public record OrderResultDto(
    Guid Id, string ResultValue, string Unit,
    string ReferenceRange, bool IsAbnormal, DateTime ResultDate);

public record CreateOrderDto(
    Guid VisitId, string? Notes,
    IEnumerable<CreateOrderDetailDto> Details);

public record CreateOrderDetailDto(string TestCode, string TestName);

public record AddOrderResultDto(
    Guid OrderDetailId, string ResultValue,
    string Unit, string ReferenceRange, bool IsAbnormal = false);
