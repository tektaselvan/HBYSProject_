using HBYS.Core.Domain.Enums;

namespace HBYS.Core.Domain.Entities;

public class Invoice
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid VisitId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public decimal SGKAmount { get; private set; }
    public decimal PatientAmount { get; private set; }
    public InvoiceStatus Status { get; private set; } = InvoiceStatus.Draft;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? SentAt { get; private set; }

    public Visit? Visit { get; private set; }
    public ICollection<InvoiceDetail> Details { get; private set; } = new List<InvoiceDetail>();

    private Invoice() { }

    public static Invoice Create(Guid visitId)
        => new() { VisitId = visitId };

    public void AddDetail(string itemType, Guid itemId, decimal unitPrice, int quantity)
    {
        var detail = InvoiceDetail.Create(Id, itemType, itemId, unitPrice, quantity);
        Details.Add(detail);
        Recalculate();
    }

    public void Send()
    {
        Status = InvoiceStatus.Sent;
        SentAt = DateTime.UtcNow;
    }

    public void MarkAsPaid() => Status = InvoiceStatus.Paid;
    public void Cancel() => Status = InvoiceStatus.Cancelled;

    private void Recalculate()
    {
        TotalAmount = Details.Sum(d => d.TotalPrice);
        SGKAmount = TotalAmount * 0.70m;
        PatientAmount = TotalAmount * 0.30m;
    }
}
