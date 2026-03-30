using HBYS.Core.Application.Interfaces;
using HBYS.Core.Domain.Entities;
using HBYS.Core.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace HBYS.Core.Infrastructure.Repositories;

public class OrderRepository(HbysDbContext context) : IOrderRepository
{
    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Orders.FindAsync([id], ct);

    public async Task<Order?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default)
        => await context.Orders
            .Include(o => o.OrderDetails).ThenInclude(d => d.Result)
            .FirstOrDefaultAsync(o => o.Id == id, ct);

    public async Task<IEnumerable<Order>> GetByVisitIdAsync(Guid visitId, CancellationToken ct = default)
        => await context.Orders
            .Include(o => o.OrderDetails).ThenInclude(d => d.Result)
            .Where(o => o.VisitId == visitId)
            .ToListAsync(ct);

    public async Task AddAsync(Order order, CancellationToken ct = default)
    {
        await context.Orders.AddAsync(order, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Order order, CancellationToken ct = default)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync(ct);
    }

    public async Task AddResultAsync(OrderResult result, CancellationToken ct = default)
    {
        await context.OrderResults.AddAsync(result, ct);
        await context.SaveChangesAsync(ct);
    }
}
