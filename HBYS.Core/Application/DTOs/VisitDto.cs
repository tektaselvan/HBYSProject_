using HBYS.Core.Domain.Enums;

namespace HBYS.Core.Application.DTOs;

public record VisitDto(
    Guid Id, Guid PatientId, string PatientName,
    string ProtocolNo, DateTime VisitDate,
    Guid DepartmentId, string DepartmentName,
    Guid DoctorId, string DoctorName,
    VisitStatus Status, string? Notes);

public record CreateVisitDto(
    Guid PatientId, Guid DepartmentId, string DepartmentName,
    Guid DoctorId, string DoctorName);
