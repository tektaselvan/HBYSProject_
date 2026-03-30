namespace HBYS.Core.Domain.Entities;

public class InvoiceDetail
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid InvoiceId { get; private set; }
    public string ItemType { get; private set; } = string.Empty;
    public Guid ItemId { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice => UnitPrice * Quantity;

    public Invoice? Invoice { get; private set; }

    private InvoiceDetail() { }

    public static InvoiceDetail Create(Guid invoiceId, string itemType, Guid itemId, decimal unitPrice, int quantity)
        => new() { InvoiceId = invoiceId, ItemType = itemType, ItemId = itemId, UnitPrice = unitPrice, Quantity = quantity };
}
