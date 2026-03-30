using HBYS.Core.Application.Interfaces;
using HBYS.Core.Domain.Entities;
using HBYS.Core.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace HBYS.Core.Infrastructure.Repositories;

public class VisitRepository(HbysDbContext context) : IVisitRepository
{
    public async Task<Visit?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Visits.FindAsync([id], ct);

    public async Task<Visit?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default)
        => await context.Visits
            .Include(v => v.Patient)
            .Include(v => v.Orders).ThenInclude(o => o.OrderDetails).ThenInclude(d => d.Result)
            .Include(v => v.Diagnoses)
            .Include(v => v.Invoice).ThenInclude(i => i!.Details)
            .FirstOrDefaultAsync(v => v.Id == id, ct);

    public async Task<IEnumerable<Visit>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default)
        => await context.Visits
            .Include(v => v.Patient)
            .Where(v => v.PatientId == patientId)
            .OrderByDescending(v => v.VisitDate)
            .ToListAsync(ct);

    public async Task<IEnumerable<Visit>> GetTodaysVisitsAsync(CancellationToken ct = default)
    {
        var today = DateTime.Today;
        return await context.Visits
            .Include(v => v.Patient)
            .Where(v => v.VisitDate.Date == today)
            .OrderBy(v => v.VisitDate)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Visit visit, CancellationToken ct = default)
    {
        await context.Visits.AddAsync(visit, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Visit visit, CancellationToken ct = default)
    {
        context.Visits.Update(visit);
        await context.SaveChangesAsync(ct);
    }
}
