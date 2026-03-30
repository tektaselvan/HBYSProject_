namespace HBYS.Core.Domain.ValueObjects;

public record Money(decimal Amount, string Currency = "TRY")
{
    public static Money Zero => new(0);
    public Money Add(Money other) => new(Amount + other.Amount, Currency);
    public Money Subtract(Money other) => new(Amount - other.Amount, Currency);
    public override string ToString() => $"{Amount:N2} {Currency}";
}
