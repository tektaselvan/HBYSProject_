namespace HBYS.Core.Domain.ValueObjects;

public record TcNo
{
    public string Value { get; }

    public TcNo(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length != 11 || !value.All(char.IsDigit))
            throw new ArgumentException("Geçersiz TC Kimlik Numarası.");
        Value = value;
    }

    public override string ToString() => Value;
}
