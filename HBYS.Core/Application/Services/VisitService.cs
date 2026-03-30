using HBYS.Core.Application.DTOs;
using HBYS.Core.Application.Interfaces;
using HBYS.Core.Domain.Entities;

namespace HBYS.Core.Application.Services;

public class VisitService(IVisitRepository visitRepository, IPatientRepository patientRepository)
{
    public async Task<VisitDto> CreateAsync(CreateVisitDto dto, CancellationToken ct = default)
    {
        var patient = await patientRepository.GetByIdAsync(dto.PatientId, ct)
            ?? throw new KeyNotFoundException($"Hasta bulunamadı: {dto.PatientId}");

        var visit = Visit.Create(dto.PatientId, dto.DepartmentId, dto.DepartmentName,
            dto.DoctorId, dto.DoctorName);

        await visitRepository.AddAsync(visit, ct);
        return ToDto(visit, patient.FullName);
    }

    public async Task<IEnumerable<VisitDto>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default)
    {
        var visits = await visitRepository.GetByPatientIdAsync(patientId, ct);
        return visits.Select(v => ToDto(v, v.Patient?.FullName ?? ""));
    }

    public async Task<IEnumerable<VisitDto>> GetTodaysVisitsAsync(CancellationToken ct = default)
    {
        var visits = await visitRepository.GetTodaysVisitsAsync(ct);
        return visits.Select(v => ToDto(v, v.Patient?.FullName ?? ""));
    }

    public async Task<VisitDto> CompleteAsync(Guid id, CancellationToken ct = default)
    {
        var visit = await visitRepository.GetByIdAsync(id, ct)
            ?? throw new KeyNotFoundException($"Vizit bulunamadı: {id}");
        visit.Complete();
        await visitRepository.UpdateAsync(visit, ct);
        return ToDto(visit, visit.Patient?.FullName ?? "");
    }

    public async Task<VisitDto> CancelAsync(Guid id, CancellationToken ct = default)
    {
        var visit = await visitRepository.GetByIdAsync(id, ct)
            ?? throw new KeyNotFoundException($"Vizit bulunamadı: {id}");
        visit.Cancel();
        await visitRepository.UpdateAsync(visit, ct);
        return ToDto(visit, visit.Patient?.FullName ?? "");
    }

    private static VisitDto ToDto(Visit v, string patientName) => new(
        v.Id, v.PatientId, patientName, v.ProtocolNo, v.VisitDate,
        v.DepartmentId, v.DepartmentName, v.DoctorId, v.DoctorName,
        v.Status, v.Notes);
}
