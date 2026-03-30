using HBYS.Core.Application.Interfaces;
using HBYS.Core.Domain.Entities;
using HBYS.Core.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace HBYS.Core.Infrastructure.Repositories;

public class InvoiceRepository(HbysDbContext context) : IInvoiceRepository
{
    public async Task<Invoice?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Invoices.Include(i => i.Details).FirstOrDefaultAsync(i => i.Id == id, ct);

    public async Task<Invoice?> GetByVisitIdAsync(Guid visitId, CancellationToken ct = default)
        => await context.Invoices.Include(i => i.Details).FirstOrDefaultAsync(i => i.VisitId == visitId, ct);

    public async Task AddAsync(Invoice invoice, CancellationToken ct = default)
    {
        await context.Invoices.AddAsync(invoice, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Invoice invoice, CancellationToken ct = default)
    {
        context.Invoices.Update(invoice);
        await context.SaveChangesAsync(ct);
    }
}
