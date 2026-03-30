using HBYS.Core.Domain.Enums;

namespace HBYS.Core.Domain.Entities;

public class OrderDetail
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid OrderId { get; private set; }
    public string TestCode { get; private set; } = string.Empty;
    public string TestName { get; private set; } = string.Empty;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public Order? Order { get; private set; }
    public OrderResult? Result { get; private set; }

    private OrderDetail() { }

    public static OrderDetail Create(Guid orderId, string testCode, string testName)
        => new() { OrderId = orderId, TestCode = testCode, TestName = testName };

    public void MarkAsCompleted() => Status = OrderStatus.Completed;
}
