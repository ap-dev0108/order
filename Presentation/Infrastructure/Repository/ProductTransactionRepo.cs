using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repo;

public class ProductTransactionRepo : IProductTransaction
{
    private readonly AppDbContext _db;

    public ProductTransactionRepo(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<InventoryTransaction>> GetInventoryTransactionsAsync()
    {
        return await _db.InventoryTransactions.AsNoTracking().ToListAsync();
    }

    public async Task<InventoryTransaction> GetTransactionAsync(Guid transactionId)
    {
        return await _db.InventoryTransactions.FirstOrDefaultAsync(transaction => transaction.Id == transactionId);
    }

    public async Task AddInventoryTransaction(InventoryTransaction inventoryTransaction)
    {
        _db.InventoryTransactions.Add(inventoryTransaction);
    }
}