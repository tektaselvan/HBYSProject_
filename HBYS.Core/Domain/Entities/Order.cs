using HBYS.Core.Domain.Enums;

namespace HBYS.Core.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid VisitId { get; private set; }
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public string? Notes { get; private set; }

    public Visit? Visit { get; private set; }
    public ICollection<OrderDetail> OrderDetails { get; private set; } = new List<OrderDetail>();

    private Order() { }

    public static Order Create(Guid visitId, string? notes = null)
        => new() { VisitId = visitId, Notes = notes };

    public void MarkAsSent() => Status = OrderStatus.Sent;
    public void MarkAsCompleted() => Status = OrderStatus.Completed;
    public void Cancel() => Status = OrderStatus.Cancelled;

    public void AddDetail(string testCode, string testName)
        => OrderDetails.Add(OrderDetail.Create(Id, testCode, testName));
}
