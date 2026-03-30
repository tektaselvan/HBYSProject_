using HBYS.Core.Domain.Enums;

namespace HBYS.Core.Domain.Entities;

public class Visit
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PatientId { get; private set; }
    public string ProtocolNo { get; private set; } = string.Empty;
    public DateTime VisitDate { get; private set; } = DateTime.UtcNow;
    public Guid DepartmentId { get; private set; }
    public string DepartmentName { get; private set; } = string.Empty;
    public Guid DoctorId { get; private set; }
    public string DoctorName { get; private set; } = string.Empty;
    public VisitStatus Status { get; private set; } = VisitStatus.Waiting;
    public string? Notes { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public Patient? Patient { get; private set; }
    public ICollection<Order> Orders { get; private set; } = new List<Order>();
    public ICollection<Diagnosis> Diagnoses { get; private set; } = new List<Diagnosis>();
    public Invoice? Invoice { get; private set; }

    private Visit() { }

    public static Visit Create(Guid patientId, Guid departmentId, string departmentName,
        Guid doctorId, string doctorName)
    {
        return new Visit
        {
            PatientId = patientId,
            ProtocolNo = GenerateProtocolNo(),
            DepartmentId = departmentId,
            DepartmentName = departmentName,
            DoctorId = doctorId,
            DoctorName = doctorName
        };
    }

    public void Complete() => Status = VisitStatus.Completed;
    public void Cancel() => Status = VisitStatus.Cancelled;
    public void SetInProgress() => Status = VisitStatus.InProgress;
    public void AddNote(string note) => Notes = note;

    private static string GenerateProtocolNo()
        => $"P{DateTime.Now:yyyyMMdd}{new Random().Next(1000, 9999)}";
}
