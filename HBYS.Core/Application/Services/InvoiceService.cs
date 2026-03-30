using HBYS.Core.Application.DTOs;
using HBYS.Core.Application.Interfaces;
using HBYS.Core.Domain.Entities;

namespace HBYS.Core.Application.Services;

public class InvoiceService(IInvoiceRepository invoiceRepository)
{
    public async Task<InvoiceDto> CreateAsync(CreateInvoiceDto dto, CancellationToken ct = default)
    {
        var existing = await invoiceRepository.GetByVisitIdAsync(dto.VisitId, ct);
        if (existing is not null)
            throw new InvalidOperationException("Bu vizit için zaten fatura oluşturulmuş.");

        var invoice = Invoice.Create(dto.VisitId);
        await invoiceRepository.AddAsync(invoice, ct);
        return ToDto(invoice);
    }

    public async Task<InvoiceDto> AddDetailAsync(Guid invoiceId, AddInvoiceDetailDto dto, CancellationToken ct = default)
    {
        var invoice = await invoiceRepository.GetByIdAsync(invoiceId, ct)
            ?? throw new KeyNotFoundException($"Fatura bulunamadı: {invoiceId}");

        invoice.AddDetail(dto.ItemType, dto.ItemId, dto.UnitPrice, dto.Quantity);
        await invoiceRepository.UpdateAsync(invoice, ct);
        return ToDto(invoice);
    }

    public async Task<InvoiceDto> SendToSGKAsync(Guid invoiceId, CancellationToken ct = default)
    {
        var invoice = await invoiceRepository.GetByIdAsync(invoiceId, ct)
            ?? throw new KeyNotFoundException($"Fatura bulunamadı: {invoiceId}");

        invoice.Send();
        await invoiceRepository.UpdateAsync(invoice, ct);
        return ToDto(invoice);
    }

    public async Task<InvoiceDto?> GetByVisitIdAsync(Guid visitId, CancellationToken ct = default)
    {
        var invoice = await invoiceRepository.GetByVisitIdAsync(visitId, ct);
        return invoice is null ? null : ToDto(invoice);
    }

    private static InvoiceDto ToDto(Invoice i) => new(
        i.Id, i.VisitId, i.TotalAmount, i.SGKAmount, i.PatientAmount, i.Status, i.CreatedAt,
        i.Details.Select(d => new InvoiceDetailDto(d.Id, d.ItemType, d.ItemId, d.UnitPrice, d.Quantity, d.TotalPrice)));
}
