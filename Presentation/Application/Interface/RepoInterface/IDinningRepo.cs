using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interface;

public interface IDinningRepo
{
    Task<List<DinningSession>> GetDinningSessionsAsync();
    Task<DinningSession> GetDinningSessionById(Guid id);
    Task AddDinning(DinningSession dinningSession);
    Task RemoveDinning(DinningSession dinningSession);
}