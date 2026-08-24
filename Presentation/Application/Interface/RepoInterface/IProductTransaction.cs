using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interface;

public interface IProductTransaction
{
    Task<List<InventoryTransaction>> GetInventoryTransactionsAsync();
    Task<InventoryTransaction> GetTransactionAsync(Guid transactionID);

    Task AddInventoryTransaction(InventoryTransaction inventoryTransaction);
}