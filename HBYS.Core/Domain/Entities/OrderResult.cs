namespace HBYS.Core.Domain.Entities;

public class OrderResult
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid OrderDetailId { get; private set; }
    public string ResultValue { get; private set; } = string.Empty;
    public string Unit { get; private set; } = string.Empty;
    public string ReferenceRange { get; private set; } = string.Empty;
    public bool IsAbnormal { get; private set; }
    public DateTime ResultDate { get; private set; } = DateTime.UtcNow;

    public OrderDetail? OrderDetail { get; private set; }

    private OrderResult() { }

    public static OrderResult Create(Guid orderDetailId, string resultValue,
        string unit, string referenceRange, bool isAbnormal = false)
        => new()
        {
            OrderDetailId = orderDetailId,
            ResultValue = resultValue,
            Unit = unit,
            ReferenceRange = referenceRange,
            IsAbnormal = isAbnormal
        };
}
