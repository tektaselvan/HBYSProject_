namespace HBYS.Core.Domain.Entities;

public class Diagnosis
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid VisitId { get; private set; }
    public string IcdCode { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool IsPrimary { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public Visit? Visit { get; private set; }

    private Diagnosis() { }

    public static Diagnosis Create(Guid visitId, string icdCode, string description, bool isPrimary = false)
        => new() { VisitId = visitId, IcdCode = icdCode, Description = description, IsPrimary = isPrimary };
}
