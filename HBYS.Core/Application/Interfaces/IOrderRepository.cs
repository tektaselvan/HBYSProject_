using HBYS.Core.Domain.Entities;

namespace HBYS.Core.Application.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Order?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Order>> GetByVisitIdAsync(Guid visitId, CancellationToken ct = default);
    Task AddAsync(Order order, CancellationToken ct = default);
    Task UpdateAsync(Order order, CancellationToken ct = default);
    Task AddResultAsync(OrderResult result, CancellationToken ct = default);
}
