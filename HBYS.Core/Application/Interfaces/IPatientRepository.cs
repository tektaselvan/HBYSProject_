using HBYS.Core.Domain.Entities;

namespace HBYS.Core.Application.Interfaces;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Patient?> GetByTcNoAsync(string tcNo, CancellationToken ct = default);
    Task<IEnumerable<Patient>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Patient>> SearchAsync(string keyword, CancellationToken ct = default);
    Task AddAsync(Patient patient, CancellationToken ct = default);
    Task UpdateAsync(Patient patient, CancellationToken ct = default);
    Task<bool> ExistsByTcNoAsync(string tcNo, CancellationToken ct = default);
}
