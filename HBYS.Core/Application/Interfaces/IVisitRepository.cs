using HBYS.Core.Domain.Entities;

namespace HBYS.Core.Application.Interfaces;

public interface IVisitRepository
{
    Task<Visit?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Visit?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Visit>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    Task<IEnumerable<Visit>> GetTodaysVisitsAsync(CancellationToken ct = default);
    Task AddAsync(Visit visit, CancellationToken ct = default);
    Task UpdateAsync(Visit visit, CancellationToken ct = default);
}
