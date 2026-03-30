using HBYS.Core.Domain.Enums;

namespace HBYS.Core.Application.DTOs;

public record PatientDto(
    Guid Id, string TcNo, string FirstName, string LastName,
    string FullName, DateTime BirthDate, int Age, Gender Gender,
    string? Phone, string? Email, string? Address);

public record CreatePatientDto(
    string TcNo, string FirstName, string LastName,
    DateTime BirthDate, Gender Gender,
    string? Phone = null, string? Email = null);

public record UpdatePatientDto(
    string FirstName, string LastName,
    string? Phone, string? Email, string? Address);
