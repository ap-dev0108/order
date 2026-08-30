using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interface;

public interface IDinningRepo
{
    Task<List<DinningSession>> GetDinningSessionsAsync();
    Task<DinningSession> GetDinningSessionById(Guid id);
    Task<bool> HasActiveSession(Guid tableId);
    Task AddDinning(DinningSession dinningSession);
}