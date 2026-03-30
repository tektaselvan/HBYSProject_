using HBYS.Core.Domain.Enums;

namespace HBYS.Core.Application.DTOs;

public record InvoiceDto(
    Guid Id, Guid VisitId, decimal TotalAmount,
    decimal SGKAmount, decimal PatientAmount,
    InvoiceStatus Status, DateTime CreatedAt,
    IEnumerable<InvoiceDetailDto> Details);

public record InvoiceDetailDto(
    Guid Id, string ItemType, Guid ItemId,
    decimal UnitPrice, int Quantity, decimal TotalPrice);

public record CreateInvoiceDto(Guid VisitId);

public record AddInvoiceDetailDto(
    string ItemType, Guid ItemId, decimal UnitPrice, int Quantity);
