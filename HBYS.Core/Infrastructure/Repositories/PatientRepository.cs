using HBYS.Core.Application.Interfaces;
using HBYS.Core.Domain.Entities;
using HBYS.Core.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace HBYS.Core.Infrastructure.Repositories;

public class PatientRepository(HbysDbContext context) : IPatientRepository
{
    public async Task<Patient?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Patients.FindAsync([id], ct);

    public async Task<Patient?> GetByTcNoAsync(string tcNo, CancellationToken ct = default)
        => await context.Patients.FirstOrDefaultAsync(p => p.TcNo == tcNo, ct);

    public async Task<IEnumerable<Patient>> GetAllAsync(CancellationToken ct = default)
        => await context.Patients.OrderBy(p => p.LastName).ToListAsync(ct);

    public async Task<IEnumerable<Patient>> SearchAsync(string keyword, CancellationToken ct = default)
        => await context.Patients.Where(p =>
            p.TcNo.Contains(keyword) ||
            p.FirstName.Contains(keyword) ||
            p.LastName.Contains(keyword)).ToListAsync(ct);

    public async Task AddAsync(Patient patient, CancellationToken ct = default)
    {
        await context.Patients.AddAsync(patient, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Patient patient, CancellationToken ct = default)
    {
        context.Patients.Update(patient);
        await context.SaveChangesAsync(ct);
    }

    public async Task<bool> ExistsByTcNoAsync(string tcNo, CancellationToken ct = default)
        => await context.Patients.AnyAsync(p => p.TcNo == tcNo, ct);
}
