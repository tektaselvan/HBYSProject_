using HBYS.Core.Application.DTOs;
using HBYS.Core.Application.Interfaces;
using HBYS.Core.Domain.Entities;

namespace HBYS.Core.Application.Services;

public class PatientService(IPatientRepository repository)
{
    public async Task<IEnumerable<PatientDto>> GetAllAsync(CancellationToken ct = default)
    {
        var patients = await repository.GetAllAsync(ct);
        return patients.Select(ToDto);
    }

    public async Task<PatientDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var patient = await repository.GetByIdAsync(id, ct);
        return patient is null ? null : ToDto(patient);
    }

    public async Task<PatientDto?> GetByTcNoAsync(string tcNo, CancellationToken ct = default)
    {
        var patient = await repository.GetByTcNoAsync(tcNo, ct);
        return patient is null ? null : ToDto(patient);
    }

    public async Task<IEnumerable<PatientDto>> SearchAsync(string keyword, CancellationToken ct = default)
    {
        var patients = await repository.SearchAsync(keyword, ct);
        return patients.Select(ToDto);
    }

    public async Task<PatientDto> CreateAsync(CreatePatientDto dto, CancellationToken ct = default)
    {
        if (await repository.ExistsByTcNoAsync(dto.TcNo, ct))
            throw new InvalidOperationException($"'{dto.TcNo}' TC numaralı hasta zaten kayıtlı.");

        var patient = Patient.Create(dto.TcNo, dto.FirstName, dto.LastName,
            dto.BirthDate, dto.Gender, dto.Phone, dto.Email);

        await repository.AddAsync(patient, ct);
        return ToDto(patient);
    }

    public async Task<PatientDto> UpdateAsync(Guid id, UpdatePatientDto dto, CancellationToken ct = default)
    {
        var patient = await repository.GetByIdAsync(id, ct)
            ?? throw new KeyNotFoundException($"Hasta bulunamadı: {id}");

        patient.Update(dto.FirstName, dto.LastName, dto.Phone, dto.Email, dto.Address);
        await repository.UpdateAsync(patient, ct);
        return ToDto(patient);
    }

    private static PatientDto ToDto(Patient p) => new(
        p.Id, p.TcNo, p.FirstName, p.LastName, p.FullName,
        p.BirthDate, p.Age, p.Gender, p.Phone, p.Email, p.Address);
}
